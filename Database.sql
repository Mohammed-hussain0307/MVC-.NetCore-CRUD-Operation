USE userData;

CREATE TABLE userDemo(
			id INT PRIMARY KEY IDENTITY,
			full_name VARCHAR(30) NOT NULL,
			date_of_birth DATE,
			email VARCHAR UNIQUE NOT NULL,
			mobile_number VARCHAR(10) UNIQUE
);

CREATE PROCEDURE SPR_User
AS
BEGIN
	SELECT * FROM userDemo
END;

CREATE PROCEDURE SPC_User(
	@FullName VARCHAR(30),
	@DateOfBirth DATE,
	@Email VARCHAR(30),
	@MobileNumber VARCHAR(10)
)
AS
BEGIN
	INSERT INTO userDemo(full_name,date_of_birth,email,mobile_number) 
				VALUES(@FullName,@DateOfBirth,@Email,@MobileNumber)
END;

CREATE PROCEDURE SPG_User @Id INT
AS
BEGIN
	SELECT * FROM userDemo WHERE id = @Id
END;

CREATE PROCEDURE SPU_User(
	@FullName VARCHAR(30),
	@DateOfBirth DATE,
	@Email VARCHAR(30),
	@MobileNumber VARCHAR(10)
)
AS
BEGIN
	UPDATE userDemo SET full_name = @FullName,
						date_of_birth = @DateOfBirth,
						email = @Email,
						mobile_number = @MobileNumber
END;

CREATE PROCEDURE SPD_User @Id INT
AS
BEGIN
	DELETE FROM userDemo WHERE id = @Id
END;
