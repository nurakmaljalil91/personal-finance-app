CREATE TABLE users (
	serial_id SERIAL,
    user_id VARCHAR(50) PRIMARY KEY NOT NULL,
    fullname TEXT,
	username VARCHAR(50),
	password VARCHAR(50),
	email VARCHAR(50),
    status INT,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TABLE accounts (
	serial_id SERIAL  ,
    account_id VARCHAR(50) PRIMARY KEY NOT NULL,
    user_id VARCHAR(50),
    account_name TEXT,
    balance NUMERIC,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT fk_users
        FOREIGN KEY(user_id)
            REFERENCES users(user_id)
            ON DELETE CASCADE
);

CREATE TABLE calendars (
	serial_id SERIAL  ,
    calendar_id VARCHAR(50) PRIMARY KEY NOT NULL,
    user_id VARCHAR(50),
    calendar_name TEXT,
    calendar_details TEXT,
    calendar_start TEXT,
    calendar_end TEXT, 
    calendar_color TEXT,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT fk_users
        FOREIGN KEY(user_id)
            REFERENCES users(user_id)
            ON DELETE CASCADE
);


CREATE TABLE chatrooms (
	serial_id SERIAL,
    chat_id VARCHAR(50) PRIMARY KEY NOT NULL,
    user_id VARCHAR(50),
    message TEXT,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT fk_users
        FOREIGN KEY(user_id)
            REFERENCES users(user_id)
            ON DELETE CASCADE
);


CREATE OR REPLACE FUNCTION trigger_set_timestamp()
RETURNS TRIGGER AS $$
BEGIN
  NEW.updated_at = NOW();
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER set_timestamp
BEFORE UPDATE ON accounts
FOR EACH ROW
EXECUTE PROCEDURE trigger_set_timestamp();

CREATE TABLE transactions (
	serial_id SERIAL  ,
    transaction_id VARCHAR(50) PRIMARY KEY NOT NULL,
    user_id VARCHAR(50),
    account_id VARCHAR(50),
    transaction_name TEXT,
    transaction_type SMALLINT,
    expense_type VARCHAR(50),
    transaction_place VARCHAR(50),
    transaction_date VARCHAR(50),
    total NUMERIC,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    image_link TEXT,
    CONSTRAINT fk_users
        FOREIGN KEY(user_id)
            REFERENCES users(user_id)
            ON DELETE CASCADE
);

CREATE TABLE history (
	serial_id SERIAL  ,
    history_id VARCHAR(50) PRIMARY KEY NOT NULL,
    account_id VARCHAR(50) NOT NULL,
    user_id VARCHAR(50) NOT NULL,
    account_name TEXT,
    balance NUMERIC,
    total_debit NUMERIC,
    total_credit NUMERIC,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TABLE todos (
	serial_id SERIAL  ,
    todo_id VARCHAR(50) PRIMARY KEY NOT NULL,
    user_id VARCHAR(50) NOT NULL,
    description TEXT NOT NULL,
	complete BOOLEAN NOT NULL,
	complete_date TIMESTAMP NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
	CONSTRAINT fk_users
       FOREIGN KEY(user_id)
           REFERENCES users(user_id)
           ON DELETE CASCADE
);

INSERT INTO todos(todo_id, user_id, description, complete) VALUES ('d13b970c-872a-4574-90c4-5c489657ce3e', 'MYAKMAL001', 'Fill ATMS for this week and last week', false);

INSERT INTO users(user_id, fullname, username, password,email,status) VALUES ('MYAKMAL001','Nur Akmal Jalil', 'arcmole007', '12345678','nurakmaljalil91@gmail.com',1);
INSERT INTO users(user_id, fullname, username, password,email,status) VALUES ('MYTEST001','Test_01', 'test01', '12345678','test1@tmail.com',2);
INSERT INTO users(user_id, fullname, username, password,email,status) VALUES ('MYTEST002','Test_02', 'test02', '12345678','test2@tmail.com',2);

-- INSERT INTO accounts (account_id ,user_id ,account_name ,balance ) VALUES ('WALLET001','MYORI001', 'Wallet', 283213000.00);
INSERT INTO accounts (account_id ,user_id ,account_name ,balance ) VALUES ('MAY151034691870','MYAKMAL001', 'MAYBANK Saving Account-i', 8184.57);
INSERT INTO accounts (account_id ,user_id ,account_name ,balance ) VALUES ('MAY501422245175','MYAKMAL001', 'MAYBANK Premeier 1 Account', 650.40);
INSERT INTO accounts (account_id ,user_id ,account_name ,balance ) VALUES ('WALLET001','MYAKMAL001', 'Wallet',72.95);
INSERT INTO accounts (account_id ,user_id ,account_name ,balance ) VALUES ('HSBC368197620025','MYAKMAL001', 'HSBC Basic Savings Account',58.71);

INSERT INTO accounts (account_id ,user_id ,account_name ,balance ) VALUES ('MAY001','MYTEST001', 'MAYBANK SAVING ACCOUNT', 283213000.00);
INSERT INTO accounts (account_id ,user_id ,account_name ,balance ) VALUES ('CIMB001','MYTEST002', 'CIMB SAVING ACCOUNT', 1000.00);

INSERT INTO transactions ( transaction_id , user_id,account_id ,transaction_name,transaction_type,expense_type,transaction_place,transaction_date,total ) VALUES (
    'bb498235-661a-409a-a9f2-607c188b029c', 
    'MYTEST001',
    'MAY001',
    'Popcorn',
    0,
    'Food',
    'Petronas',
    '2020-11-21',
    20.00
);

INSERT INTO  transactions ( transaction_id ,user_id,account_id ,transaction_name,transaction_type,expense_type,transaction_place,transaction_date,total ) VALUES (
    '77bc30bd-d0a5-4023-9c2c-3996d56534ae', 
    'MYTEST002',
     'CIMB001',
    'Wonda Cofee',
    0,
    'Food',
    '4.5',
    '2020-11-21',
    20.00
);

INSERT INTO  transactions ( transaction_id ,user_id,account_id ,transaction_name,transaction_type,expense_type,transaction_place,transaction_date,total ) VALUES (
    'f7929fed-8abf-4846-be84-e2d5e9925fe7', 
       'MYTEST002',
    'CIMB001',
    'Wonda Cofee',
    0,
    'Food',
    '4.5',
    '2020-11-21',
    20.00
);

INSERT INTO  transactions ( transaction_id ,user_id,account_id ,transaction_name,transaction_type,expense_type,transaction_place,transaction_date,total ) VALUES (
    '0fa59c6e-3722-4314-8568-08fa60ecb8c6', 
    'MYAKMAL001',
    'WALLET001',
    'Roti kosong, roti telur, roti kaya, kopi tarik',
    0,
    'Food',
    'Bismi Bistro',
    '2020-11-21',
    8.1
);

UPDATE  transactions SET transaction_id= ,account_id= ,transaction_name=,transaction_type=,expense_type=,transaction_place=,total=,transaction_date= ;

INSERT INTO  history ( history_id,account_id, user_id,account_name,balance,total_debit,total_credit) VALUES (
    '131120UPD',
    'MAY001', 
    'MYTEST001',
    'MAYBANK SAVING ACCOUNT',
    1234123.00,
    1231.00,
    21312.00
); 


-- Drop all tables
DROP SCHEMA public CASCADE;
CREATE SCHEMA public;
-- If you are using PostgreSQL 9.3 or greater, you may also need to restore the default grants.
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO public;

{
	"id": "MYTEST005",
	"fullname": "Test_05",
	"username": "test05",
	"password": "1345678",
	"email": "test5@tmail.com",
	"status": 2
}

select exists(select 1 from users where user_id='MYAKMAL001')

CREATE TABLE subscriptions (
	serial_id SERIAL  ,
    subscription_id VARCHAR(50) PRIMARY KEY NOT NULL,
    user_id VARCHAR(50),
    account_id VARCHAR(50),
    name TEXT,
    total NUMERIC,
	subscription_type VARCHAR(50),
	every VARCHAR(50),
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT fk_users
        FOREIGN KEY(user_id)
            REFERENCES users(user_id)
            ON DELETE CASCADE
);

INSERT INTO  subscriptions ( subscription_id,user_id,account_id, name,total,subscription_type,every) VALUES (
    'MICROSOFT2481931296',
    'MYAKMAL001', 
    'MAY151034691870',
    'Microsoft 365 Personal',
    269.00,
    'Yearly',
    '2020-12-01'
); 

INSERT INTO  subscriptions ( subscription_id,user_id,account_id, name,total,subscription_type,every) VALUES (
    'SPOTIFYPREMIUM001',
    'MYAKMAL001', 
    'HSBC368197620025',
    'Spotify Premium Plan',
    14.90,
    'Monthly',
    '11-29'
); 

UPDATE todos SET complete=true, complete_date='' WHERE todo_id='';