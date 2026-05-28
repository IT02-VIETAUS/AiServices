using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VA.ErpAiClient.Wpf.Models;
using VA.ErpAiClient.Wpf.Services;

namespace VA.ErpAiClient.Wpf.Views;

public partial class AiChatControl : UserControl
{
    private AiApiClient? _client;
    private Guid? _sessionId;

    public AiChatControl()
    {
        InitializeComponent();
    }

    // Gọi hàm này sau khi user đăng nhập ERP.
    // baseUrl ví dụ: http://localhost:5088
    // erpAccessToken lấy từ phiên đăng nhập ERP hiện có, không tạo login riêng cho AI API.
    public void InitializeAiClient(string baseUrl, string erpAccessToken)
    {
        _client?.Dispose();
        _client = new AiApiClient(baseUrl, erpAccessToken);
        AddSystemMessage("Kết nối AI API đã sẵn sàng.");
    }

    private async void BtnSend_Click(object sender, RoutedEventArgs e)
    {
        await SendCurrentMessageAsync();
    }

    private async void TxtMessage_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && Keyboard.Modifiers == ModifierKeys.Control)
        {
            e.Handled = true;
            await SendCurrentMessageAsync();
        }
    }

    private async Task SendCurrentMessageAsync()
    {
        if (_client is null)
        {
            AddSystemMessage("Chưa cấu hình kết nối AI API.");
            return;
        }

        var text = txtMessage.Text.Trim();
        if (string.IsNullOrWhiteSpace(text)) return;

        txtMessage.Clear();
        AddUserMessage(text);
        SetBusy(true);

        try
        {
            var response = await _client.SendChatAsync(new AiChatRequest
            {
                SessionId = _sessionId,
                Message = text
            });

            _sessionId = response.SessionId;
            AddAiMessage(response.Answer, response.Model);
        }
        catch (Exception ex)
        {
            AddSystemMessage("Không thể gọi AI API: " + ex.Message);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void SetBusy(bool isBusy)
    {
        btnSend.IsEnabled = !isBusy;
        txtMessage.IsEnabled = !isBusy;
        btnSend.Content = isBusy ? "Đang gửi..." : "Gửi";
    }

    private void AddUserMessage(string message)
    {
        AddBubble("Người dùng", message, "#DBEAFE", "#111827", HorizontalAlignment.Right);
    }

    private void AddAiMessage(string message, string model)
    {
        AddBubble($"AI ({model})", message, "#ECFDF5", "#064E3B", HorizontalAlignment.Left);
    }

    private void AddSystemMessage(string message)
    {
        AddBubble("Hệ thống", message, "#FEF3C7", "#78350F", HorizontalAlignment.Center);
    }

    private void AddBubble(string title, string message, string background, string foreground, HorizontalAlignment align)
    {
        var border = new Border
        {
            Background = (Brush)new BrushConverter().ConvertFromString(background),
            CornerRadius = new CornerRadius(10),
            Padding = new Thickness(10),
            Margin = new Thickness(0, 0, 0, 10),
            MaxWidth = 760,
            HorizontalAlignment = align
        };

        var stack = new StackPanel();
        stack.Children.Add(new TextBlock
        {
            Text = title,
            FontWeight = FontWeights.SemiBold,
            Foreground = (Brush)new BrushConverter().ConvertFromString(foreground),
            Margin = new Thickness(0, 0, 0, 4)
        });

        stack.Children.Add(new TextBlock
        {
            Text = message,
            TextWrapping = TextWrapping.Wrap,
            FontSize = 14,
            Foreground = (Brush)new BrushConverter().ConvertFromString(foreground)
        });

        border.Child = stack;
        ChatPanel.Children.Add(border);
        ChatScroll.ScrollToEnd();
    }
}
