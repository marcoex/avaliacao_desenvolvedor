CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Comprador] VARCHAR(100) NULL, 
    [Descricao] VARCHAR(200) NULL, 
    [Endereco] VARCHAR(300) NULL, 
    [Fornecedor] VARCHAR(100) NULL, 
    [PrecoUnitario] MONEY NULL, 
    [Quantidade] SMALLINT NULL
)
