from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy.future import select
from sqlalchemy.orm import selectinload

from app.models.ticket import Ticket
from app.schemas.ticket import TicketCreate, TicketUpdate

class CRUDTicket:
    async def get(self, db: AsyncSession, id: int) -> Ticket | None:
        result = await db.execute(
            select(Ticket).options(selectinload(Ticket.created_by), selectinload(Ticket.assignee)).where(Ticket.id == id)
        )
        return result.scalars().first()

    async def get_multi(self, db: AsyncSession, *, skip: int = 0, limit: int = 100) -> list[Ticket]:
        result = await db.execute(
            select(Ticket)
            .options(selectinload(Ticket.created_by), selectinload(Ticket.assignee))
            .offset(skip)
            .limit(limit)
        )
        return result.scalars().all()

    async def create(self, db: AsyncSession, *, obj_in: TicketCreate, created_by_id: str) -> Ticket:
        db_obj = Ticket(
            **obj_in.dict(),
            created_by_id=created_by_id
        )
        db.add(db_obj)
        await db.commit()
        await db.refresh(db_obj)
        return db_obj

    async def update(
        self, db: AsyncSession, *, db_obj: Ticket, obj_in: TicketUpdate
    ) -> Ticket:
        update_data = obj_in.dict(exclude_unset=True)
        for field in update_data:
            setattr(db_obj, field, update_data[field])

        db.add(db_obj)
        await db.commit()
        await db.refresh(db_obj)
        return db_obj

    async def remove(self, db: AsyncSession, *, id: int) -> Ticket | None:
        result = await db.execute(select(Ticket).where(Ticket.id == id))
        db_obj = result.scalars().first()
        if db_obj:
            await db.delete(db_obj)
            await db.commit()
        return db_obj

ticket = CRUDTicket()
