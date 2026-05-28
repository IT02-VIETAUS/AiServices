# Persistence layer

Giai đoạn 1 chưa tạo bảng DB để tránh sinh schema vội khi chưa có kết nối thật.

Từ giai đoạn 2 sẽ thêm migration/script cho:

- ai.documents
- ai.document_chunks
- ai.chat_sessions
- ai.chat_messages
- ai.chat_retrieval_sources
- ai.agent_runs
- ai.agent_steps
- ai.agent_tool_calls
- ai.agent_approvals
- ai.audit_logs
