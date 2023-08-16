Create table ChowNow_User(
	user_id integer IDENTITY(1,1) NOT NULL,
	username varchar(30) NOT NULL UNIQUE,
	password varchar(300) NOT NULL,
	email varchar(100) NOT NULL UNIQUE,
	user_type varchar(30),
	first_name varchar(30) NOT NULL,
	last_name varchar(30) NOT NULL,
	birth_date dateTime NOT NULL,
	address varchar(30) NOT NULL,
	image varchar(200),
	enabled varchar(5) NOT NULL,
	CONSTRAINT USER_PK PRIMARY KEY(user_id)
)

Create table ChowNow_Order(
	order_id integer IDENTITY(1,1) NOT NULL,
	total_price float NOT NULL,
	ammount integer NOT NULL,
	comment varchar(100),
	delivery_address varchar(100) NOT NULL,
	status varchar(5) NOT NULL,
	user_id integer NOT NULL REFERENCES ChowNow_User(user_id),
	CONSTRAINT ORDER_PK PRIMARY KEY(order_id)
)

Create table ChowNow_Product(
	product_id integer IDENTITY(1,1) NOT NULL,
	product_name varchar(100) NOT NULL,
	price float NOT NULL,
	ingredients varchar(200) NOT NULL,
	CONSTRAINT PRODUCT_PK PRIMARY KEY(product_id) 
)

Create table ChowNow_Contains(
	contains_id integer IDENTITY(1,1) NOT NULL,
	order_id integer NOT NULL REFERENCES ChowNow_Order(order_id),
	product_id integer NOT NULL REFERENCES ChowNow_Product(product_id),
	CONSTRAINT CONTAINS_PK PRIMARY KEY(contains_id) 
)

