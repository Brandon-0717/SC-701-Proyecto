CREATE DATABASE SGC_SC701_PROYECTO;
GO
USE SGC_SC701_PROYECTO;

/*----------------------------------------------------------------------------------*/
GO
CREATE TABLE ESTADOS(
	ESTADOS_PK UNIQUEIDENTIFIER NOT NULL,--ES UN GUID
	Nombre_Estado NVARCHAR(128) NOT NULL,
	--Auditoria
	CreadoPor NVARCHAR(128),
	ModificadoPor NVARCHAR(128),
	Fecha_Modificacion DATETIME,
	--
	CONSTRAINT ESTADOS_PK PRIMARY KEY (Estados_PK)
);
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
	--Adicional al Identity
	[Estados_FK_AspNetUsers] uniqueidentifier NULL,
	[Nombre] nvarchar(256) NOT NULL,
	[PrimerApellido] nvarchar(256) NULL,
	[SegundoApellido] nvarchar(256) NULL,
	[Identificacion] nvarchar(256) unique NOT NULL,
	[FechaNacimiento] datetime NULL,
	[FotoPerfilUrl] nvarchar(512) NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Eestado_usuario] FOREIGN KEY ([Estados_FK_AspNetUsers]) REFERENCES ESTADOS(ESTADOS_PK)
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'00000000000000_CreateIdentitySchema', N'8.0.21');
GO

COMMIT;
GO
/*---------------------------------------------------------------------------------*/
--INSERT DE IDENTITY
INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
VALUES 
('00000000-0000-0000-0000-000000000001', 'Administrador', 'ADMINISTRADOR', NEWID()),
('00000000-0000-0000-0000-000000000002', 'Empleado', 'EMPLEADO', NEWID()),
('00000000-0000-0000-0000-000000000003', 'Analista', 'ANALISTA', NEWID()),
('00000000-0000-0000-0000-000000000004', 'Gestor', 'GESTOR', NEWID()),
('00000000-0000-0000-0000-000000000005', 'Cliente', 'CLIENTE', NEWID());

GO

INSERT INTO ESTADOS (ESTADOS_PK, Nombre_Estado, CreadoPor, ModificadoPor, Fecha_Modificacion)
VALUES
('00000000-0000-0000-0000-000000000001', 'Activo', 'Sistema', NULL, GETDATE()),
('00000000-0000-0000-0000-000000000002', 'Inactivo', 'Sistema', NULL, GETDATE()),
('00000000-0000-0000-0000-000000000003', 'Pendiente', 'Sistema', NULL, GETDATE()),
('00000000-0000-0000-0000-000000000004', 'Suspendido', 'Sistema', NULL, GETDATE()),
('00000000-0000-0000-0000-000000000005', 'Eliminado', 'Sistema', NULL, GETDATE());
GO

-- INSERT del usuario ADMIN 
INSERT INTO AspNetUsers
(
    Id,
    UserName,
    NormalizedUserName,
    Email,
    NormalizedEmail,
    EmailConfirmed,
    PasswordHash,
    SecurityStamp,
    ConcurrencyStamp,
    PhoneNumberConfirmed,
    TwoFactorEnabled,
    LockoutEnabled,
    AccessFailedCount,
    Estados_FK_AspNetUsers,
    Nombre,
    PrimerApellido,
    SegundoApellido,
    Identificacion,
    FechaNacimiento,
    FotoPerfilUrl
)
VALUES
(
    '11111111-1111-1111-1111-111111111111', -- Id del usuario
    'admin@sgc.com',                         -- UserName
    'ADMIN@SGC.COM',                         -- NormalizedUserName
    'admin@sgc.com',                         -- Email
    'ADMIN@SGC.COM',                         -- NormalizedEmail
    1,                                       -- EmailConfirmed
    'AQAAAAIAAYagAAAAENWdbaxj3SPIQff5Ehy0RsNiroVo0R0uKIZLrlo2ukXxQOiQU9TFk9ICTUDHJqUWkA==',-- PasswordHash //LA CONTRASEÑA ES: Jeager-0717
    NEWID(),                                 -- SecurityStamp
    NEWID(),                                 -- ConcurrencyStamp
    0,                                       -- PhoneNumberConfirmed
    0,                                       -- TwoFactorEnabled
    0,                                       -- LockoutEnabled
    0,                                       -- AccessFailedCount
    '00000000-0000-0000-0000-000000000001', -- Estado Activo
    'Admin',                                 -- Nombre
    'Sistema',                               -- PrimerApellido
    'SGC',                                   -- SegundoApellido
    '0000000001',                             -- Identificacion
    '2004-12-17',                            -- FechaNacimiento
    ''   -- FotoPerfilUrl
);
GO
-- Asignar rol de Administrador
INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES
(
    '11111111-1111-1111-1111-111111111111', -- Id del usuario insertado
    '00000000-0000-0000-0000-000000000001'  -- Id del rol Administrador
);
GO


/*---------------------------------------------------------------------------------*/
/*TABLAS*/

CREATE TABLE CLIENTES(
	CLIENTES_PK UNIQUEIDENTIFIER NOT NULL,
	ESTADOS_FK_CLIENTES UNIQUEIDENTIFIER NOT NULL,
	--
	Cedula INT NOT NULL,
	Primer_Nombre NVARCHAR(128) NOT NULL,
	Segundo_Nombre NVARCHAR(128) NOT NULL,
	Primer_Apellido NVARCHAR(128) NOT NULL,
	Segundo_Apellido NVARCHAR(128) NOT NULL,
	Fecha_Nacimiento DATETIME NOT NULL,
	Telefono NVARCHAR(128) NOT NULL,
	Correo_Electronico NVARCHAR(128) NOT NULL,
	Sexo BIT NOT NULL, --EN VEZ DE BOOL
	Direccion_Exacta NVARCHAR(128) NOT NULL,
	Fecha_Creacion DATETIME NOT NULL,
	--Auditoria
	CreadoPor NVARCHAR(128),
	ModificadoPor NVARCHAR(128),
	Fecha_Modificacion DATETIME,
	--
	CONSTRAINT CLIENTES_PK PRIMARY KEY (CLIENTES_PK),
	CONSTRAINT ESTADOS_FK_CLIENTES FOREIGN KEY (ESTADOS_FK_CLIENTES) REFERENCES ESTADOS (ESTADOS_PK)
);
GO
CREATE TABLE CATEGORIAS_CREDITO(
	CATEGORIAS_CREDITO_PK UNIQUEIDENTIFIER NOT NULL,
	Nombre_Categoria NVARCHAR(128) NOT NULL,
	Descripcion NVARCHAR(128) NOT NULL,
	--Auditoria
	CreadoPor NVARCHAR(128),
	ModificadoPor NVARCHAR(128),
	Fecha_Modificacion DATETIME,
	--
	CONSTRAINT CATEGORIAS_CREDITO_PK PRIMARY KEY (CATEGORIAS_CREDITO_PK)
);
GO
CREATE TABLE SOLICITUDES_CREDITO(
	SOLICITUDES_CREDITO_PK UNIQUEIDENTIFIER NOT NULL,
	ESTADOS_FK_SOLICITUDES_CREDITO UNIQUEIDENTIFIER NOT NULL,
	CATEGORIAS_CREDITO_FK_SOLICITUDES_CREDITO UNIQUEIDENTIFIER NOT NULL,
	UserId_FK_SOLICITUES_CREDITO NVARCHAR(450) NOT NULL,--OJO CON LOS ID DE LOS USUARIOS IDENTITY
	CLIENTES_FK_SOLICITUES_CREDITO UNIQUEIDENTIFIER NOT NULL,
	--
	Monto_Credito DECIMAL(18,2) NOT NULL,
	Comentario NVARCHAR(256) NOT NULL,
	--Auditoria
	CreadoPor NVARCHAR(128),
	ModificadoPor NVARCHAR(128),
	Fecha_Modificacion DATETIME,
	--
	CONSTRAINT SOLICITUDES_CREDITO_PK PRIMARY KEY (SOLICITUDES_CREDITO_PK),
	CONSTRAINT ESTADOS_FK_SOLICITUDES_CREDITO FOREIGN KEY (ESTADOS_FK_SOLICITUDES_CREDITO) REFERENCES ESTADOS(ESTADOS_PK),
	CONSTRAINT CATEGORIAS_CREDITO_FK_SOLICITUDES_CREDITO FOREIGN KEY (CATEGORIAS_CREDITO_FK_SOLICITUDES_CREDITO) REFERENCES CATEGORIAS_CREDITO(CATEGORIAS_CREDITO_PK),
	CONSTRAINT UserId_FK_SOLICITUES_CREDITO FOREIGN KEY (UserId_FK_SOLICITUES_CREDITO) REFERENCES AspNetUsers(Id),
	CONSTRAINT CLIENTES_FK_SOLICITUES_CREDITO FOREIGN KEY (CLIENTES_FK_SOLICITUES_CREDITO) REFERENCES CLIENTES(CLIENTES_PK)
);
GO
CREATE TABLE ARCHIVOS_CREDITO(
	ARCHIVOS_CREDITO_PK UNIQUEIDENTIFIER NOT NULL,
	SOLICITUDES_CREDITO_FK_ARCHIVOS_CREDITO UNIQUEIDENTIFIER NOT NULL,
	UserId_FK_ARCHIVOS_CREDITO NVARCHAR(450) NOT NULL,
	--
	Nombre_Archivo NVARCHAR(128) NOT NULL,
	Url_Archivo NVARCHAR(128) NOT NULL,
	Fecha_Creacion DATETIME NOT NULL,
	--Auditoria
	CreadoPor NVARCHAR(128),
	ModificadoPor NVARCHAR(128),
	Fecha_Modificacion DATETIME,
	--
	CONSTRAINT ARCHIVOS_CREDITO_PK PRIMARY KEY (ARCHIVOS_CREDITO_PK),
	CONSTRAINT SOLICITUDES_CREDITO_FK_ARCHIVOS_CREDITO FOREIGN KEY (SOLICITUDES_CREDITO_FK_ARCHIVOS_CREDITO) REFERENCES SOLICITUDES_CREDITO(SOLICITUDES_CREDITO_PK),
	CONSTRAINT UserId_FK_ARCHIVOS_CREDITO FOREIGN KEY (UserId_FK_ARCHIVOS_CREDITO) REFERENCES AspNetUsers(Id)
);
GO
CREATE TABLE GESTIONES_CREDITO(
	GESTIONES_CREDITO_PK UNIQUEIDENTIFIER NOT NULL,
	UserId_FK_GESTIONES_CREDITO NVARCHAR(450) NOT NULL,
	ESTADOS_FK_GESTIONES_CREDITO UNIQUEIDENTIFIER NOT NULL,
	SOLICITUDES_CREDITO_FK_GESTIONES_CREDITO UNIQUEIDENTIFIER NOT NULL,
	--
	Comentario NVARCHAR(128) NOT NULL,
	FECHA_CREACION DATETIME NOT NULL,
	--Auditoria
	CreadoPor NVARCHAR(128),
	ModificadoPor NVARCHAR(128),
	Fecha_Modificacion DATETIME,
	--
	CONSTRAINT GESTIONES_CREDITO_PK PRIMARY KEY (GESTIONES_CREDITO_PK),
	CONSTRAINT UserId_FK_GESTIONES_CREDITO FOREIGN KEY (UserId_FK_GESTIONES_CREDITO) REFERENCES AspNetUsers(Id),
	CONSTRAINT ESTADOS_FK_GESTIONES_CREDITO FOREIGN KEY (ESTADOS_FK_GESTIONES_CREDITO) REFERENCES ESTADOS(ESTADOS_PK),
	CONSTRAINT SOLICITUDES_CREDITO_FK_GESTIONES_CREDITO FOREIGN KEY (SOLICITUDES_CREDITO_FK_GESTIONES_CREDITO) REFERENCES SOLICITUDES_CREDITO(SOLICITUDES_CREDITO_PK)
);
GO
CREATE TABLE HST_GESTIONES_CREDITO(
	HST_GESTIONES_CREDITO_PK UNIQUEIDENTIFIER NOT NULL,
	GESTIONES_CREDITO_FK_HST_GESTIONES_CREDITO UNIQUEIDENTIFIER NOT NULL,
	UserId_FK_HST_GESTIONES_CREDITO NVARCHAR(450) NOT NULL,
	ESTADO_ANTERIOR_FK UNIQUEIDENTIFIER NOT NULL,
	ESTADO_NUEVO_FK UNIQUEIDENTIFIER NOT NULL,
	--
	Comentario NVARCHAR(256) NOT NULL,
	Fecha_Creacion DATETIME NOT NULL,
	--Auditoria
	CreadoPor NVARCHAR(128),
	ModificadoPor NVARCHAR(128),
	Fecha_Modificacion DATETIME,
	--
	CONSTRAINT HST_GESTIONES_CREDITO_PK PRIMARY KEY (HST_GESTIONES_CREDITO_PK),
	CONSTRAINT GESTIONES_CREDITO_FK_HST_GESTIONES_CREDITO FOREIGN KEY (GESTIONES_CREDITO_FK_HST_GESTIONES_CREDITO) REFERENCES GESTIONES_CREDITO(GESTIONES_CREDITO_PK),
	CONSTRAINT UserId_FK_HST_GESTIONES_CREDITO FOREIGN KEY (UserId_FK_HST_GESTIONES_CREDITO) REFERENCES AspNetUsers(Id),
	CONSTRAINT ESTADO_ANTERIOR_FK FOREIGN KEY (ESTADO_ANTERIOR_FK) REFERENCES ESTADOS(ESTADOS_PK),
	CONSTRAINT ESTADO_NUEVO_FK FOREIGN KEY (ESTADO_NUEVO_FK) REFERENCES ESTADOS(ESTADOS_PK)
);
GO


