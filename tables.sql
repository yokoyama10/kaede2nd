# CREATE DATABASE ennichidb DEFAULT CHARACTER SET utf8;

CREATE TABLE operator (
	operator_id INT UNSIGNED AUTO_INCREMENT NOT NULL, #受付者ID
	operator_name VARCHAR(255) UNIQUE NOT NULL, #名前
	operator_comment TEXT default NULL, #コメント

	PRIMARY KEY ( operator_id )
) type=InnoDB;

CREATE TABLE external ( #生徒でない人
	external_id INT UNSIGNED AUTO_INCREMENT NOT NULL, #外部者ID
	external_type CHAR(2) default NULL, #種別
	external_name VARCHAR(255) UNIQUE NOT NULL, #名前
	external_comment TEXT default NULL, #コメント

	PRIMARY KEY ( external_id )
) type=InnoDB;

CREATE TABLE receipt (
	receipt_id INT UNSIGNED AUTO_INCREMENT NOT NULL, #受付書ID
	receipt_pass CHAR(8) default NULL, #パスワード
	receipt_seller CHAR(4) NOT NULL default "9999", #売り手コード（出席番号）
###	receipt_seller_external INT UNSIGNED default NULL, #外部id
	receipt_seller_exname VARCHAR(64) default NULL, #名前
	receipt_seller_branch INT UNSIGNED default NULL, #枝番
	receipt_time DATETIME default NULL, #受付時刻
	receipt_operator INT UNSIGNED default NULL, #受付者
	receipt_payback BOOL default FALSE, #返金済みか NULL: 不明
###	receipt_loss BOOL NOT NULL default FALSE, #紛失
	receipt_comment VARCHAR(255) default NULL, #コメント

	PRIMARY KEY ( receipt_id ),
###	FOREIGN KEY ( receipt_seller_external ) REFERENCES external(external_id) ON DELETE SET NULL,
	FOREIGN KEY ( receipt_operator ) REFERENCES operator(operator_id) ON DELETE SET NULL

) type=InnoDB;

CREATE TABLE item (
	item_id INT UNSIGNED AUTO_INCREMENT NOT NULL, #商品ID
	item_receipt_id INT UNSIGNED NOT NULL, #受付書ID
	item_name VARCHAR(255) NOT NULL, #商品名
	item_tagprice INT UNSIGNED NOT NULL default 0, #定価
	item_tataki BOOL NOT NULL default FALSE, #タタキ
	item_return BOOL NOT NULL default TRUE, #返品
	item_genre VARCHAR(255) default NULL, #ジャンル !temp
	item_sellway VARCHAR(255) default NULL, #販売方法 !temp

	item_receipt_time DATETIME default NULL, #受付時刻（代替）
	item_receipt_operator INT UNSIGNED default NULL, #受付者（代替）

	item_sellprice INT UNSIGNED default NULL, #売価
	item_selltime DATETIME default NULL, #販売時刻
	item_sell_operator INT UNSIGNED default NULL, #販売入力者
	item_adjust INT SIGNED default NULL, #事故時調整額

	item_isbn BIGINT(20) UNSIGNED default NULL, #ISBNコード
	item_comment VARCHAR(255) default NULL, #商品コメント
	item_sellcomment TEXT default NULL, #販売コメント、事故詳細
	item_userspace TEXT default NULL, #自由領域

	PRIMARY KEY ( item_id ),
	FOREIGN KEY ( item_receipt_id ) REFERENCES receipt(receipt_id) ON DELETE CASCADE,
	FOREIGN KEY ( item_receipt_operator ) REFERENCES operator(operator_id) ON DELETE SET NULL,
	FOREIGN KEY ( item_sell_operator ) REFERENCES operator(operator_id) ON DELETE SET NULL

) type=InnoDB;
