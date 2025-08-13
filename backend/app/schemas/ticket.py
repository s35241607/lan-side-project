import uuid
from datetime import datetime
from pydantic import BaseModel
from .user import User  # Import User schema for nesting

# Shared properties
class TicketBase(BaseModel):
    title: str
    description: str | None = None
    priority: str = 'MEDIUM'
    status: str = 'PENDING'

# Properties to receive on ticket creation
class TicketCreate(TicketBase):
    assignee_id: uuid.UUID | None = None

# Properties to receive on ticket update
class TicketUpdate(BaseModel):
    title: str | None = None
    description: str | None = None
    priority: str | None = None
    status: str | None = None
    assignee_id: uuid.UUID | None = None

# Properties shared by models stored in DB
class TicketInDBBase(TicketBase):
    id: int
    created_by_id: uuid.UUID
    assignee_id: uuid.UUID | None
    created_at: datetime
    updated_at: datetime | None

    class Config:
        orm_mode = True

# Properties to return to client
class Ticket(TicketInDBBase):
    created_by: User
    assignee: User | None = None

# Properties stored in DB
class TicketInDB(TicketInDBBase):
    pass
