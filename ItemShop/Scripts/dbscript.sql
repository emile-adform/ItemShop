create table shops (
	id serial PRIMARY KEY,
	name varchar(50),
	address varchar(200)
);

create table items (
	id serial PRIMARY KEY,
	name varchar(50),
	price decimal,
	shop_id int,
	FOREIGN KEY (shop_id) REFERENCES id(shops)
);

create table purchases (
	id serial PRIMARY KEY,
	user_id int,
	item_id int,
	FOREIGN KEY (item_id) REFERENCES id(items)
);