# CREATE DATABASE ennichidb;

CREATE TABLE operator (
	operator_id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
	operator_name VARCHAR(255) UNIQUE NOT NULL,
	operator_comment TEXT default NULL
);

CREATE TABLE config (
	config_name char(128) PRIMARY KEY NOT NULL,
	config_value text NOT NULL
);

CREATE TABLE receipt (
	receipt_id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
	receipt_pass CHAR(8) default NULL,
	receipt_seller CHAR(4) NOT NULL default "9999",
	receipt_seller_exname VARCHAR(64) default NULL,
	receipt_time DATETIME default NULL,
	receipt_operator INTEGER default NULL,
	receipt_payback BOOL default FALSE,
	receipt_comment VARCHAR(255) default NULL,

	FOREIGN KEY ( receipt_operator ) REFERENCES operator(operator_id) ON DELETE SET NULL
);

CREATE TABLE item (
	item_id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
	item_receipt_id INTEGER NOT NULL,
	item_name VARCHAR(255) NOT NULL,
	item_tagprice INTEGER NOT NULL default 0,
	item_tataki BOOL NOT NULL default FALSE,
	item_return BOOL NOT NULL default TRUE,
	item_genre VARCHAR(255) default NULL,
	item_sellway VARCHAR(255) default NULL,

	item_receipt_time DATETIME default NULL,
	item_receipt_operator INTEGER default NULL,

	item_sellprice INTEGER default NULL,
	item_selltime DATETIME default NULL,
	item_sell_operator INTEGER default NULL,

	item_kansa_end DATETIME default NULL,
	item_kansa_flag1 INTEGER default NULL,
	
	item_adjust INTEGER default NULL,

	item_isbn TEXT default NULL,
	item_volumes INTEGER default NULL,

	item_comment VARCHAR(255) default NULL,
	item_sellcomment TEXT default NULL,
	item_userspace TEXT default NULL,

	FOREIGN KEY ( item_receipt_id ) REFERENCES receipt(receipt_id) ON DELETE CASCADE,
	FOREIGN KEY ( item_receipt_operator ) REFERENCES operator(operator_id) ON DELETE SET NULL,
	FOREIGN KEY ( item_sell_operator ) REFERENCES operator(operator_id) ON DELETE SET NULL

);
