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
    account_id VARCHAR(50),
    transaction_name TEXT,
    transaction_type SMALLINT,
    expense_type VARCHAR(50),
    transaction_place VARCHAR(50),
    total NUMERIC,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    CONSTRAINT fk_accounts
        FOREIGN KEY(account_id)
            REFERENCES accounts(account_id)
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

INSERT INTO transactions ( transaction_id ,account_id ,transaction_name,transaction_type,expense_type,transaction_place,total ) VALUES (
    '131120F001Qwh537', 
    'MAY001',
    'Popcorn',
    0,
    'Food',
    'Petronas',
    20.00
);

INSERT INTO  transactions ( transaction_id ,account_id ,transaction_name,transaction_type,expense_type,transaction_place,total ) VALUES (
    '131120F001Qwd547', 
    'CIMB001',
    'Wonda Cofee',
    0,
    'Food',
    '4.5',
    20.00
);

INSERT INTO  transactions ( transaction_id ,account_id ,transaction_name,transaction_type,expense_type,transaction_place,total ) VALUES (
    '131120F001Qwd547', 
    'CIMB001',
    'Wonda Cofee',
    0,
    'Food',
    '4.5',
    20.00
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