SET default_tablespace = pg_default;

CREATE TABLE IF NOT EXISTS hero
(
    id smallint NOT NULL GENERATED ALWAYS AS IDENTITY,
    name varchar(50) NOT NULL
);

INSERT INTO hero(name)
	VALUES 
	('Deadpool'),
    ('Thor'),
    ('Captain America'),
    ('Hulk'),
    ('Wolverine'),
	('Mystic');
