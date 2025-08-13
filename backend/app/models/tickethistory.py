from sqlalchemy import Column, Integer, String, Text, ForeignKey, DateTime
from sqlalchemy.orm import relationship
from sqlalchemy.sql import func
from sqlalchemy.dialects.postgresql import UUID

from app.db.base import Base

class TicketHistory(Base):
    __tablename__ = 'tickethistories'

    id = Column(Integer, primary_key=True, index=True)
    ticket_id = Column(Integer, ForeignKey("tickets.id"), nullable=False)
    changed_by_id = Column(UUID(as_uuid=True), ForeignKey("users.id"))
    field_changed = Column(String(100), nullable=False)
    old_value = Column(Text)
    new_value = Column(Text)
    changed_at = Column(DateTime(timezone=True), server_default=func.now())

    ticket = relationship("Ticket", back_populates="history")
    changed_by = relationship("User")
