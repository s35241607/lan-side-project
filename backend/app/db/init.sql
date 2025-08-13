-- This is a basic schema to get started.
-- For a real-world application, you would use a migration tool like Alembic.

-- Extensions
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- User table (even with SSO, you might want to store user info)
CREATE TABLE IF NOT EXISTS users (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    username VARCHAR(50) UNIQUE NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    full_name VARCHAR(100),
    is_active BOOLEAN DEFAULT TRUE,
    is_superuser BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- Ticket table
CREATE TABLE IF NOT EXISTS tickets (
    id SERIAL PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    status VARCHAR(50) NOT NULL DEFAULT 'PENDING', -- PENDING, IN_PROGRESS, WAITING_FOR_APPROVAL, COMPLETED, REJECTED
    priority VARCHAR(50) DEFAULT 'MEDIUM', -- LOW, MEDIUM, HIGH
    created_by_id UUID REFERENCES users(id),
    assignee_id UUID REFERENCES users(id),
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- Approval table
CREATE TABLE IF NOT EXISTS approvals (
    id SERIAL PRIMARY KEY,
    ticket_id INTEGER NOT NULL REFERENCES tickets(id) ON DELETE CASCADE,
    approver_id UUID NOT NULL REFERENCES users(id),
    status VARCHAR(50) NOT NULL DEFAULT 'PENDING', -- PENDING, APPROVED, REJECTED
    comments TEXT,
    sequence_order INTEGER NOT NULL, -- Defines the order of approval
    is_current BOOLEAN DEFAULT FALSE, -- Indicates if this is the current approval step
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    UNIQUE (ticket_id, sequence_order)
);

-- Ticket History table
CREATE TABLE IF NOT EXISTS tickethistories (
    id SERIAL PRIMARY KEY,
    ticket_id INTEGER NOT NULL REFERENCES tickets(id) ON DELETE CASCADE,
    changed_by_id UUID REFERENCES users(id),
    field_changed VARCHAR(100) NOT NULL,
    old_value TEXT,
    new_value TEXT,
    changed_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- Create some initial data for testing
INSERT INTO users (username, email, full_name, is_superuser) VALUES
('johndoe', 'john.doe@example.com', 'John Doe', false),
('janedoe', 'jane.doe@example.com', 'Jane Doe', false),
('admin', 'admin@example.com', 'Admin User', true)
ON CONFLICT (username) DO NOTHING;
