import uuid
from datetime import datetime
from pydantic import BaseModel
from .user import User

# Shared properties
class ApprovalBase(BaseModel):
    status: str = 'PENDING'
    comments: str | None = None

# Properties to receive on approval creation
class ApprovalCreate(BaseModel):
    ticket_id: int
    approver_id: uuid.UUID
    sequence_order: int

# Properties to receive on approval update (e.g., when an approver acts on it)
class ApprovalUpdate(BaseModel):
    status: str
    comments: str | None = None

# Properties shared by models stored in DB
class ApprovalInDBBase(ApprovalBase):
    id: int
    ticket_id: int
    approver_id: uuid.UUID
    sequence_order: int
    created_at: datetime
    updated_at: datetime | None

    class Config:
        orm_mode = True

# Properties to return to client
class Approval(ApprovalInDBBase):
    approver: User

# Properties stored in DB
class ApprovalInDB(ApprovalInDBBase):
    pass
