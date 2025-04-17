CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk___ef_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;
CREATE TABLE users (
    id uuid NOT NULL,
    email text NOT NULL,
    username text NOT NULL,
    password text NOT NULL,
    role text NOT NULL,
    CONSTRAINT pk_users PRIMARY KEY (id)
);

INSERT INTO users (id, email, password, role, username)
VALUES ('325e7e30-d656-403e-82da-9c54149299de', 'user2@gmail.com', '$2a$11$Nq4ythN8eBgXXpsGo7MuK.92XlQhS64P/T5SC69ozqXhaDGqIkbdy', 'user', 'user2');
INSERT INTO users (id, email, password, role, username)
VALUES ('3b4f23bd-975d-4a69-9999-f8ea8a6d5b7c', 'admin@gmail.com', '$2a$11$Nq4ythN8eBgXXpsGo7MuK.92XlQhS64P/T5SC69ozqXhaDGqIkbdy', 'admin', 'admin');
INSERT INTO users (id, email, password, role, username)
VALUES ('b2464355-d637-46e6-8843-c3bd8fdf88fa', 'user1@gmail.com', '$2a$11$Nq4ythN8eBgXXpsGo7MuK.92XlQhS64P/T5SC69ozqXhaDGqIkbdy', 'user', 'user1');

INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20250417125124_Initial', '9.0.0');

COMMIT;

