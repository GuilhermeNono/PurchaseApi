CREATE TABLE Product(
    Id uniqueidentifier not null constraint PK_Product primary key default newsequentialid(),
    Name varchar(255) not null,
    Price decimal(19, 4) not null
)
