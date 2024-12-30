CREATE TABLE Product_Log
(
    Id        bigint           not null identity
        constraint PK_Product_Log primary key,
    IdProduct uniqueidentifier not null,
    Name      varchar(255)     not null,
    Price     decimal(19, 4)   not null
)
