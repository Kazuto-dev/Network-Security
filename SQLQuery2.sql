CREATE PROCEDURE dbo.RegisterUser
    @Username VARCHAR(50),
    @Password VARCHAR(50)
AS
BEGIN
    -- Your registration logic here
    INSERT INTO tbl2 (username, password) VALUES (@Username, @Password);
    INSERT INTO tbl (username, password) VALUES (@Username, @Password);
END;