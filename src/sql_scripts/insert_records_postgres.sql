INSERT INTO arena(name)
	VALUES 
	('Dune'), 
	('Forest'),
	('Mountains'),
	('Hills');

INSERT INTO squad(name, rank, arena_id)
	VALUES 
	('Freemen', 1, 1),
	('Sardaukar', 1, 1);

INSERT INTO hero(name, age, power, hit_points, squad_id)
	VALUES 
	('Dunkan', 33, 47, 100, 1),
	('Colonel', 28, 30, 100, 2);

INSERT INTO equipment(name, type, damage_points, hero_id)
	VALUES 
	('Knife', 'cold weapon', 15, 1),
	('Gun', 'fire weapon', 25, 2);

INSERT INTO skill(name, type, healing_points, cost, hero_id)
	VALUES 
	('concentration', 'focus', 15, 5, 1);