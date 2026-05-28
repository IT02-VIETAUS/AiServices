namespace AiServices.Application.Abstractions.Persistence.Rag;

/// <summary>
/// Giai đoạn 2 sẽ triển khai search top chunks từ PostgreSQL/pgvector.
/// Hiện tại để interface trước để AiChatService có đường mở rộng đúng kiến trúc.
/// </summary>
public interface IDocumentRetrievalService
{
    Task<IReadOnlyList<RetrievedDocumentChunk>> RetrieveAsync(
        string question,
        int topK,
        CancellationToken cancellationToken);
}

public sealed class RetrievedDocumentChunk
{
    public required Guid DocumentId { get; init; }
    public required Guid ChunkId { get; init; }
    public required string SourceName { get; init; }
    public string? SourcePath { get; init; }
    public required int ChunkNo { get; init; }
    public required string Text { get; init; }
    public required double Score { get; init; }
}
