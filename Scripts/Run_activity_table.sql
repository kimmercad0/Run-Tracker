CREATE TABLE Run_Activity(
    Run_Id INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Location NVARCHAR(100) NOT NULL,
    DateTimeStarted DATETIME NOT NULL,
    DateTimeEnded DATETIME NOT NULL,
    Distance DECIMAL(5, 2) NOT NULL,
    Duration TIME,
    AveragePace TIME,
    CONSTRAINT fk_user FOREIGN KEY (UserID) REFERENCES Users(UserID)
);