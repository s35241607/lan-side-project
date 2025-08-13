from typing import Any, List
from fastapi import APIRouter, Depends, HTTPException, Request
from sqlalchemy.ext.asyncio import AsyncSession

from app import crud, models, schemas
from app.deps import get_db, get_current_active_user
from app.services.notification.kafka_producer import KafkaProducer, get_kafka_producer

router = APIRouter()

@router.get("/", response_model=List[schemas.Ticket])
async def read_tickets(
    db: AsyncSession = Depends(get_db),
    skip: int = 0,
    limit: int = 100,
    current_user: models.User = Depends(get_current_active_user),
) -> Any:
    """
    Retrieve tickets.
    """
    tickets = await crud.ticket.get_multi(db, skip=skip, limit=limit)
    return tickets

@router.post("/", response_model=schemas.Ticket)
async def create_ticket(
    *,
    db: AsyncSession = Depends(get_db),
    ticket_in: schemas.TicketCreate,
    current_user: models.User = Depends(get_current_active_user),
    kafka_producer: KafkaProducer = Depends(get_kafka_producer),
) -> Any:
    """
    Create new ticket.
    """
    ticket = await crud.ticket.create(db=db, obj_in=ticket_in, created_by_id=current_user.id)
    await kafka_producer.send_message(
        topic="ticket_events",
        message={"event": "ticket_created", "ticket_id": ticket.id}
    )
    return ticket

@router.put("/{id}", response_model=schemas.Ticket)
async def update_ticket(
    *,
    db: AsyncSession = Depends(get_db),
    id: int,
    ticket_in: schemas.TicketUpdate,
    current_user: models.User = Depends(get_current_active_user),
) -> Any:
    """
    Update a ticket.
    """
    ticket = await crud.ticket.get(db=db, id=id)
    if not ticket:
        raise HTTPException(status_code=404, detail="Ticket not found")
    # Add authorization logic here if needed
    ticket = await crud.ticket.update(db=db, db_obj=ticket, obj_in=ticket_in)
    return ticket

@router.get("/{id}", response_model=schemas.Ticket)
async def read_ticket(
    *,
    db: AsyncSession = Depends(get_db),
    id: int,
    current_user: models.User = Depends(get_current_active_user),
) -> Any:
    """
    Get ticket by ID.
    """
    ticket = await crud.ticket.get(db=db, id=id)
    if not ticket:
        raise HTTPException(status_code=404, detail="Ticket not found")
    return ticket

@router.delete("/{id}", response_model=schemas.Ticket)
async def delete_ticket(
    *,
    db: AsyncSession = Depends(get_db),
    id: int,
    current_user: models.User = Depends(get_current_active_user),
) -> Any:
    """
    Delete a ticket.
    """
    ticket = await crud.ticket.get(db=db, id=id)
    if not ticket:
        raise HTTPException(status_code=404, detail="Ticket not found")
    # Add authorization logic here if needed
    ticket = await crud.ticket.remove(db=db, id=id)
    return ticket
