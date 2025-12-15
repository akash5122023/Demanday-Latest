USE [TenantDb];
GO

IF OBJECT_ID('dbo.usp_ProvisionTenantSignup', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_ProvisionTenantSignup;
GO

CREATE PROCEDURE dbo.usp_ProvisionTenantSignup
    @CompanyName        NVARCHAR(250),
    @CompanyAddress     NVARCHAR(500),
    @CompanyPhone       NVARCHAR(50),
    @CompanyStateName   NVARCHAR(200),
    @AdminEmail         NVARCHAR(256),
    @AdminPassword      NVARCHAR(256),
    @AdminDisplayName   NVARCHAR(100) = NULL,
    @CompanySlogan      NVARCHAR(250) = NULL,
    @CompanyCode        NVARCHAR(50) = NULL,
    @CompanyCountryCode NVARCHAR(50) = NULL,
    @Plan               NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @step NVARCHAR(200) = N'Initializing';
    DECLARE @normalizedEmail NVARCHAR(256) = LOWER(LTRIM(RTRIM(@AdminEmail)));
    DECLARE @normalizedPlan NVARCHAR(100) = NULLIF(LTRIM(RTRIM(@Plan)), '');
    DECLARE @now DATETIME2(0) = SYSUTCDATETIME();
    DECLARE @stateTableName SYSNAME = N'dbo.State';
    DECLARE @companyTableName SYSNAME;
    DECLARE @companyTableId INT;
    DECLARE @companyId INT;
    DECLARE @companyIdColumn SYSNAME;
    DECLARE @stateId INT;
    DECLARE @userId INT;
    DECLARE @existingUserId INT;
    DECLARE @roleId INT;
    DECLARE @stateNameColumn SYSNAME;
    DECLARE @selfConstraintDisabled BIT = 0;
    DECLARE @stateMinId INT;
    DECLARE @stateCount INT;

    DECLARE @salt VARBINARY(16);
    DECLARE @passwordHash VARBINARY(64);

    DECLARE @emailVerifiedExists BIT = CASE WHEN COL_LENGTH('dbo.Users', 'EmailVerified') IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @companyIdInUsersExists BIT = CASE WHEN COL_LENGTH('dbo.Users', 'CompanyId') IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @planColumnExists BIT = CASE WHEN COL_LENGTH('dbo.Users', 'Plan') IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @lastDirectoryExists BIT = CASE WHEN COL_LENGTH('dbo.Users', 'LastDirectoryUpdate') IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @insertDateExists BIT = CASE WHEN COL_LENGTH('dbo.Users', 'InsertDate') IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @insertUserIdExists BIT = CASE WHEN COL_LENGTH('dbo.Users', 'InsertUserId') IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @updateDateExists BIT = CASE WHEN COL_LENGTH('dbo.Users', 'UpdateDate') IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @updateUserIdExists BIT = CASE WHEN COL_LENGTH('dbo.Users', 'UpdateUserId') IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @createdDateExists BIT = CASE WHEN COL_LENGTH('dbo.Users', 'CreatedDate') IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @createdByExists BIT = CASE WHEN COL_LENGTH('dbo.Users', 'CreatedBy') IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @sourceColumnExists BIT = CASE WHEN COL_LENGTH('dbo.Users', 'Source') IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @isActiveExists BIT = CASE WHEN COL_LENGTH('dbo.Users', 'IsActive') IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @upperLevelExists BIT = CASE WHEN COL_LENGTH('dbo.Users', 'UpperLevel') IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @upperLevel2Exists BIT = CASE WHEN COL_LENGTH('dbo.Users', 'UpperLevel2') IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @upperLevel3Exists BIT = CASE WHEN COL_LENGTH('dbo.Users', 'UpperLevel3') IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @upperLevel4Exists BIT = CASE WHEN COL_LENGTH('dbo.Users', 'UpperLevel4') IS NOT NULL THEN 1 ELSE 0 END;
    DECLARE @upperLevel5Exists BIT = CASE WHEN COL_LENGTH('dbo.Users', 'UpperLevel5') IS NOT NULL THEN 1 ELSE 0 END;

    DECLARE @mailToOrganisationExists BIT;
    DECLARE @companyCodeExists BIT;
    DECLARE @companyCountryCodeExists BIT;
    DECLARE @companyIsActiveExists BIT;
    DECLARE @companyCreatedDateExists BIT;
    DECLARE @companyCreatedByExists BIT;
    DECLARE @companyUpdatedDateExists BIT;
    DECLARE @companyUpdatedByExists BIT;

    DECLARE @rolesCompanyIdExists BIT = CASE WHEN COL_LENGTH('dbo.Roles', 'CompanyId') IS NOT NULL THEN 1 ELSE 0 END;

    IF (@CompanyName IS NULL OR LTRIM(RTRIM(@CompanyName)) = '')
        THROW 50000, 'Company name is required.', 1;
    IF (@CompanyAddress IS NULL OR LTRIM(RTRIM(@CompanyAddress)) = '')
        THROW 50000, 'Company address is required.', 1;
    IF (@CompanyPhone IS NULL OR LTRIM(RTRIM(@CompanyPhone)) = '')
        THROW 50000, 'Company phone is required.', 1;
    IF (@CompanyStateName IS NULL OR LTRIM(RTRIM(@CompanyStateName)) = '')
        THROW 50000, 'Company state name is required.', 1;
    IF (@normalizedEmail IS NULL OR @normalizedEmail = '')
        THROW 50000, 'Admin email is required.', 1;
    IF (@AdminPassword IS NULL OR LTRIM(RTRIM(@AdminPassword)) = '')
        THROW 50000, 'Admin password is required.', 1;

    IF COL_LENGTH('dbo.Users', 'Username') IS NULL OR COL_LENGTH('dbo.Users', 'DisplayName') IS NULL OR
       COL_LENGTH('dbo.Users', 'Email') IS NULL OR COL_LENGTH('dbo.Users', 'PasswordHash') IS NULL OR
       COL_LENGTH('dbo.Users', 'PasswordSalt') IS NULL
    BEGIN
        THROW 50000, 'dbo.Users is missing required columns for provisioning.', 1;
    END

    IF OBJECT_ID('dbo.CompanyDetail', 'U') IS NOT NULL
    BEGIN
        SET @companyTableName = N'dbo.CompanyDetail';
    END
    ELSE IF OBJECT_ID('dbo.CompanyDetails', 'U') IS NOT NULL
    BEGIN
        SET @companyTableName = N'dbo.CompanyDetails';
    END
    ELSE
    BEGIN
        THROW 50000, 'Unable to locate company table (dbo.CompanyDetail or dbo.CompanyDetails).', 1;
    END;

    SET @companyTableId = OBJECT_ID(@companyTableName);

    SELECT TOP (1)
        @companyIdColumn = c.name
    FROM sys.columns AS c
    WHERE c.object_id = @companyTableId AND c.is_identity = 1
    ORDER BY c.column_id;

    IF @companyIdColumn IS NULL
        SET @companyIdColumn = N'Id';

    IF COLUMNPROPERTY(@companyTableId, 'Name', 'ColumnId') IS NULL OR COLUMNPROPERTY(@companyTableId, 'Address', 'ColumnId') IS NULL OR
       COLUMNPROPERTY(@companyTableId, 'Phone', 'ColumnId') IS NULL OR COLUMNPROPERTY(@companyTableId, 'StateId', 'ColumnId') IS NULL
    BEGIN
        THROW 50000, 'Company table is missing required columns for provisioning.', 1;
    END;

    SET @mailToOrganisationExists = CASE WHEN COLUMNPROPERTY(@companyTableId, 'MailToOrganisation', 'ColumnId') IS NOT NULL THEN 1 ELSE 0 END;
    SET @companyCodeExists = CASE WHEN COLUMNPROPERTY(@companyTableId, 'Code', 'ColumnId') IS NOT NULL THEN 1 ELSE 0 END;
    SET @companyCountryCodeExists = CASE WHEN COLUMNPROPERTY(@companyTableId, 'CountryCode', 'ColumnId') IS NOT NULL THEN 1 ELSE 0 END;
    SET @companyIsActiveExists = CASE WHEN COLUMNPROPERTY(@companyTableId, 'IsActive', 'ColumnId') IS NOT NULL THEN 1 ELSE 0 END;
    SET @companyCreatedDateExists = CASE WHEN COLUMNPROPERTY(@companyTableId, 'CreatedDate', 'ColumnId') IS NOT NULL THEN 1 ELSE 0 END;
    SET @companyCreatedByExists = CASE WHEN COLUMNPROPERTY(@companyTableId, 'CreatedBy', 'ColumnId') IS NOT NULL THEN 1 ELSE 0 END;
    SET @companyUpdatedDateExists = CASE WHEN COLUMNPROPERTY(@companyTableId, 'UpdatedDate', 'ColumnId') IS NOT NULL THEN 1 ELSE 0 END;
    SET @companyUpdatedByExists = CASE WHEN COLUMNPROPERTY(@companyTableId, 'UpdatedBy', 'ColumnId') IS NOT NULL THEN 1 ELSE 0 END;

    IF COLUMNPROPERTY(OBJECT_ID(@stateTableName), 'Name', 'ColumnId') IS NOT NULL
        SET @stateNameColumn = N'Name';
    ELSE IF COLUMNPROPERTY(OBJECT_ID(@stateTableName), 'State', 'ColumnId') IS NOT NULL
        SET @stateNameColumn = N'State';
    ELSE
        THROW 50000, 'Unable to find a valid name column on dbo.State.', 1;

    BEGIN TRY
        SET @step = N'Beginning tenant provisioning transaction';
        PRINT @step;
        BEGIN TRAN;

        SET @step = N'Truncating dbo.UserRoles';
        PRINT @step;
        IF OBJECT_ID('dbo.UserRoles', 'U') IS NOT NULL
        BEGIN
            TRUNCATE TABLE dbo.UserRoles;
            DBCC CHECKIDENT('dbo.UserRoles', RESEED, 0) WITH NO_INFOMSGS;
        END

        SET @step = N'Truncating dbo.Users';
        PRINT @step;
        IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
        BEGIN
            TRUNCATE TABLE dbo.Users;
            DBCC CHECKIDENT('dbo.Users', RESEED, 0) WITH NO_INFOMSGS;
        END

        SET @step = N'Truncating dbo.Branch';
        PRINT @step;
        IF OBJECT_ID('dbo.Branch', 'U') IS NOT NULL
        BEGIN
            TRUNCATE TABLE dbo.Branch;
            DBCC CHECKIDENT('dbo.Branch', RESEED, 0) WITH NO_INFOMSGS;
        END

        SET @step = N'Truncating company table';
        PRINT @step;
        EXEC(N'TRUNCATE TABLE ' + @companyTableName + ';');
        EXEC(N'DBCC CHECKIDENT(''' + @companyTableName + ''', RESEED, 0) WITH NO_INFOMSGS;');

        SET @step = N'Truncating dbo.State';
        PRINT @step;
        IF OBJECT_ID(@stateTableName, 'U') IS NOT NULL
        BEGIN
            EXEC(N'TRUNCATE TABLE ' + @stateTableName + ';');
            EXEC(N'DBCC CHECKIDENT(''' + @stateTableName + ''', RESEED, 0) WITH NO_INFOMSGS;');
        END

        SET @step = N'Copying states from [MainDb].dbo.State';
        PRINT @step;
        INSERT INTO dbo.State([Name], [Code], [CountryCode], [IsActive])
        SELECT m.[Name], m.[Code], m.[CountryCode], 1
        FROM [MainDb].dbo.State AS m
        WHERE ISNULL(m.IsActive, 1) = 1
        ORDER BY m.[Name];

        SET @step = N'Validating state reseed';
        PRINT @step;
        SELECT @stateMinId = MIN(Id), @stateCount = COUNT(*) FROM dbo.State;
        IF (@stateMinId <> 1 OR @stateCount = 0)
            THROW 50001, 'State reseed validation failed. Expected MIN(Id) = 1 with data present.', 1;

        SET @step = N'Resolving state identifier';
        PRINT @step;
        DECLARE @stateLookupSql NVARCHAR(MAX) =
            N'SELECT @outStateId = Id FROM dbo.State WHERE UPPER(LTRIM(RTRIM(' + QUOTENAME(@stateNameColumn) +
            N'))) = UPPER(LTRIM(RTRIM(@name)))';
        EXEC sp_executesql @stateLookupSql,
            N'@name NVARCHAR(200), @outStateId INT OUTPUT',
            @name = @CompanyStateName,
            @outStateId = @stateId OUTPUT;

        IF @stateId IS NULL
        BEGIN
            DECLARE @missingState NVARCHAR(400) = N'State ''' + @CompanyStateName + N''' not found in dbo.State.';
            THROW 50002, @missingState, 1;
        END

        SET @step = N'Inserting company details';
        PRINT @step;
        DECLARE @companyColumns NVARCHAR(MAX) = N'[Name], [Address], [Phone], [StateId]';
        DECLARE @companyValues NVARCHAR(MAX) = N'@CompanyName, @CompanyAddress, @CompanyPhone, @StateId';

        IF COLUMNPROPERTY(@companyTableId, 'Slogan', 'ColumnId') IS NOT NULL
        BEGIN
            SET @companyColumns = N'[Name], [Slogan], [Address], [Phone], [StateId]';
            SET @companyValues = N'@CompanyName, @CompanySlogan, @CompanyAddress, @CompanyPhone, @StateId';
        END

        IF @mailToOrganisationExists = 1
        BEGIN
            SET @companyColumns += N', [MailToOrganisation]';
            SET @companyValues += N', 1';
        END

        IF @companyCodeExists = 1
        BEGIN
            SET @companyColumns += N', [Code]';
            SET @companyValues += N', @CompanyCode';
        END

        IF @companyCountryCodeExists = 1
        BEGIN
            SET @companyColumns += N', [CountryCode]';
            SET @companyValues += N', @CompanyCountryCode';
        END

        IF @companyIsActiveExists = 1
        BEGIN
            SET @companyColumns += N', [IsActive]';
            SET @companyValues += N', 1';
        END

        IF @companyCreatedDateExists = 1
        BEGIN
            SET @companyColumns += N', [CreatedDate]';
            SET @companyValues += N', @Now';
        END

        IF @companyCreatedByExists = 1
        BEGIN
            SET @companyColumns += N', [CreatedBy]';
            SET @companyValues += N', NULL';
        END

        IF @companyUpdatedDateExists = 1
        BEGIN
            SET @companyColumns += N', [UpdatedDate]';
            SET @companyValues += N', @Now';
        END

        IF @companyUpdatedByExists = 1
        BEGIN
            SET @companyColumns += N', [UpdatedBy]';
            SET @companyValues += N', NULL';
        END

        DECLARE @companyInsertSql NVARCHAR(MAX) =
            N'INSERT INTO ' + @companyTableName + N'(' + @companyColumns + N') VALUES(' + @companyValues +
            N'); SELECT @outCompanyId = CAST(SCOPE_IDENTITY() AS INT);';

        EXEC sp_executesql @companyInsertSql,
            N'@CompanyName NVARCHAR(250), @CompanySlogan NVARCHAR(250), @CompanyAddress NVARCHAR(500), @CompanyPhone NVARCHAR(50),
              @StateId INT, @CompanyCode NVARCHAR(50), @CompanyCountryCode NVARCHAR(50), @Now DATETIME2(0), @outCompanyId INT OUTPUT',
            @CompanyName = @CompanyName,
            @CompanySlogan = @CompanySlogan,
            @CompanyAddress = @CompanyAddress,
            @CompanyPhone = @CompanyPhone,
            @StateId = @stateId,
            @CompanyCode = @CompanyCode,
            @CompanyCountryCode = @CompanyCountryCode,
            @Now = @now,
            @outCompanyId = @companyId OUTPUT;

        IF (@companyId IS NULL OR @companyId = 0)
            THROW 50003, 'Failed to insert company details.', 1;

        SET @step = N'Preparing admin user profile';
        PRINT @step;
        SET @AdminDisplayName = COALESCE(NULLIF(LTRIM(RTRIM(@AdminDisplayName)), ''), @CompanyName);

        SET @step = N'Generating admin password hash';
        PRINT @step;
        SET @salt = CRYPT_GEN_RANDOM(16);
        SET @passwordHash = HASHBYTES('SHA2_512', @salt + CONVERT(VARBINARY(8000), CAST(@AdminPassword AS NVARCHAR(4000))));

        SET @step = N'Checking for existing admin user';
        PRINT @step;
        SELECT TOP (1) @existingUserId = UserId
        FROM dbo.Users
        WHERE LOWER(LTRIM(RTRIM(Email))) = @normalizedEmail;

        IF @existingUserId IS NOT NULL
        BEGIN
            SET @step = N'Updating existing admin user';
            PRINT @step;
            DECLARE @userUpdateSql NVARCHAR(MAX) =
                N'UPDATE dbo.Users SET DisplayName = @DisplayName, Username = @Email, Email = @Email, ' +
                N'PasswordHash = @PasswordHash, PasswordSalt = @Salt';

            IF @isActiveExists = 1
                SET @userUpdateSql += N', IsActive = 1';
            IF @lastDirectoryExists = 1
                SET @userUpdateSql += N', LastDirectoryUpdate = @Now';
            IF @emailVerifiedExists = 1
                SET @userUpdateSql += N', EmailVerified = 1';
            IF @companyIdInUsersExists = 1
                SET @userUpdateSql += N', CompanyId = @CompanyId';
            IF @planColumnExists = 1
                SET @userUpdateSql += N', Plan = @Plan';
            IF @updateDateExists = 1
                SET @userUpdateSql += N', UpdateDate = @Now';
            IF @updateUserIdExists = 1
                SET @userUpdateSql += N', UpdateUserId = @ExistingUserId';
            IF @createdDateExists = 1
                SET @userUpdateSql += N', CreatedDate = COALESCE(CreatedDate, @Now)';
            IF @createdByExists = 1
                SET @userUpdateSql += N', CreatedBy = COALESCE(CreatedBy, @ExistingUserId)';

            SET @userUpdateSql += N' WHERE UserId = @ExistingUserId;';

            EXEC sp_executesql @userUpdateSql,
                N'@DisplayName NVARCHAR(100), @Email NVARCHAR(256), @PasswordHash VARBINARY(64), @Salt VARBINARY(16), @Now DATETIME2(0),
                  @CompanyId INT, @Plan NVARCHAR(100), @ExistingUserId INT',
                @DisplayName = @AdminDisplayName,
                @Email = @normalizedEmail,
                @PasswordHash = @passwordHash,
                @Salt = @salt,
                @Now = @now,
                @CompanyId = @companyId,
                @Plan = @normalizedPlan,
                @ExistingUserId = @existingUserId;

            SET @userId = @existingUserId;
        END
        ELSE
        BEGIN
            SET @step = N'Inserting admin user';
            PRINT @step;
            IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_UsersUserId_UserId' AND parent_object_id = OBJECT_ID('dbo.Users'))
            BEGIN
                EXEC('ALTER TABLE dbo.Users NOCHECK CONSTRAINT FK_UsersUserId_UserId;');
                SET @selfConstraintDisabled = 1;
                PRINT N'Disabled FK_UsersUserId_UserId.';
            END

            DECLARE @userInsertColumns NVARCHAR(MAX) =
                N'Username, DisplayName, Email, PasswordHash, PasswordSalt';
            DECLARE @userInsertValues NVARCHAR(MAX) =
                N'@Email, @DisplayName, @Email, @PasswordHash, @Salt';

            IF @sourceColumnExists = 1
            BEGIN
                SET @userInsertColumns += N', Source';
                SET @userInsertValues += N', ''site''';
            END
            IF @isActiveExists = 1
            BEGIN
                SET @userInsertColumns += N', IsActive';
                SET @userInsertValues += N', 1';
            END
            IF @insertDateExists = 1
            BEGIN
                SET @userInsertColumns += N', InsertDate';
                SET @userInsertValues += N', @Now';
            END
            IF @insertUserIdExists = 1
            BEGIN
                SET @userInsertColumns += N', InsertUserId';
                SET @userInsertValues += N', NULL';
            END
            IF @updateDateExists = 1
            BEGIN
                SET @userInsertColumns += N', UpdateDate';
                SET @userInsertValues += N', @Now';
            END
            IF @updateUserIdExists = 1
            BEGIN
                SET @userInsertColumns += N', UpdateUserId';
                SET @userInsertValues += N', NULL';
            END
            IF @createdDateExists = 1
            BEGIN
                SET @userInsertColumns += N', CreatedDate';
                SET @userInsertValues += N', @Now';
            END
            IF @createdByExists = 1
            BEGIN
                SET @userInsertColumns += N', CreatedBy';
                SET @userInsertValues += N', NULL';
            END
            IF @lastDirectoryExists = 1
            BEGIN
                SET @userInsertColumns += N', LastDirectoryUpdate';
                SET @userInsertValues += N', @Now';
            END
            IF @companyIdInUsersExists = 1
            BEGIN
                SET @userInsertColumns += N', CompanyId';
                SET @userInsertValues += N', @CompanyId';
            END
            IF @emailVerifiedExists = 1
            BEGIN
                SET @userInsertColumns += N', EmailVerified';
                SET @userInsertValues += N', 1';
            END
            IF @planColumnExists = 1
            BEGIN
                SET @userInsertColumns += N', Plan';
                SET @userInsertValues += N', @Plan';
            END

            DECLARE @userInsertSql NVARCHAR(MAX) =
                N'INSERT INTO dbo.Users(' + @userInsertColumns + N') VALUES(' + @userInsertValues +
                N'); SELECT @outUserId = CAST(SCOPE_IDENTITY() AS INT);';

            EXEC sp_executesql @userInsertSql,
                N'@Email NVARCHAR(256), @DisplayName NVARCHAR(100), @PasswordHash VARBINARY(64), @Salt VARBINARY(16), @Now DATETIME2(0),
                  @CompanyId INT, @Plan NVARCHAR(100), @outUserId INT OUTPUT',
                @Email = @normalizedEmail,
                @DisplayName = @AdminDisplayName,
                @PasswordHash = @passwordHash,
                @Salt = @salt,
                @Now = @now,
                @CompanyId = @companyId,
                @Plan = @normalizedPlan,
                @outUserId = @userId OUTPUT;

            IF (@userId IS NULL)
                THROW 50004, 'Failed to insert admin user.', 1;

            DECLARE @selfUpdateFragments NVARCHAR(MAX) = N'';
            IF @insertUserIdExists = 1
                SET @selfUpdateFragments += CASE WHEN LEN(@selfUpdateFragments) = 0 THEN N'' ELSE N', ' END + N'InsertUserId = @UserId';
            IF @updateUserIdExists = 1
                SET @selfUpdateFragments += CASE WHEN LEN(@selfUpdateFragments) = 0 THEN N'' ELSE N', ' END + N'UpdateUserId = @UserId';
            IF @createdByExists = 1
                SET @selfUpdateFragments += CASE WHEN LEN(@selfUpdateFragments) = 0 THEN N'' ELSE N', ' END + N'CreatedBy = @UserId';
            IF @upperLevelExists = 1
                SET @selfUpdateFragments += CASE WHEN LEN(@selfUpdateFragments) = 0 THEN N'' ELSE N', ' END + N'UpperLevel = @UserId';
            IF @upperLevel2Exists = 1
                SET @selfUpdateFragments += CASE WHEN LEN(@selfUpdateFragments) = 0 THEN N'' ELSE N', ' END + N'UpperLevel2 = @UserId';
            IF @upperLevel3Exists = 1
                SET @selfUpdateFragments += CASE WHEN LEN(@selfUpdateFragments) = 0 THEN N'' ELSE N', ' END + N'UpperLevel3 = @UserId';
            IF @upperLevel4Exists = 1
                SET @selfUpdateFragments += CASE WHEN LEN(@selfUpdateFragments) = 0 THEN N'' ELSE N', ' END + N'UpperLevel4 = @UserId';
            IF @upperLevel5Exists = 1
                SET @selfUpdateFragments += CASE WHEN LEN(@selfUpdateFragments) = 0 THEN N'' ELSE N', ' END + N'UpperLevel5 = @UserId';
            IF @updateDateExists = 1
                SET @selfUpdateFragments += CASE WHEN LEN(@selfUpdateFragments) = 0 THEN N'' ELSE N', ' END + N'UpdateDate = @Now';
            IF @createdDateExists = 1
                SET @selfUpdateFragments += CASE WHEN LEN(@selfUpdateFragments) = 0 THEN N'' ELSE N', ' END + N'CreatedDate = COALESCE(CreatedDate, @Now)';

            IF LEN(@selfUpdateFragments) > 0
            BEGIN
                SET @step = N'Updating admin self references';
                PRINT @step;
                DECLARE @selfUpdateSql NVARCHAR(MAX) =
                    N'UPDATE dbo.Users SET ' + @selfUpdateFragments + N' WHERE UserId = @UserId;';
                EXEC sp_executesql @selfUpdateSql,
                    N'@UserId INT, @Now DATETIME2(0)',
                    @UserId = @userId,
                    @Now = @now;
            END
        END

        IF @selfConstraintDisabled = 1
        BEGIN
            SET @step = N'Re-enabling FK_UsersUserId_UserId';
            PRINT @step;
            EXEC('ALTER TABLE dbo.Users WITH CHECK CHECK CONSTRAINT FK_UsersUserId_UserId;');
            SET @selfConstraintDisabled = 0;
        END

        IF @companyCreatedByExists = 1 OR @companyUpdatedByExists = 1
        BEGIN
            SET @step = N'Updating company audit references';
            PRINT @step;
            DECLARE @companyAuditSet NVARCHAR(MAX) = N'';
            IF @companyCreatedByExists = 1
                SET @companyAuditSet += CASE WHEN LEN(@companyAuditSet) = 0 THEN N'' ELSE N', ' END + N'[CreatedBy] = @UserId';
            IF @companyUpdatedByExists = 1
            BEGIN
                SET @companyAuditSet += CASE WHEN LEN(@companyAuditSet) = 0 THEN N'' ELSE N', ' END + N'[UpdatedBy] = @UserId';
                IF @companyUpdatedDateExists = 1
                    SET @companyAuditSet += N', [UpdatedDate] = @Now';
            END

            IF LEN(@companyAuditSet) > 0
            BEGIN
                DECLARE @companyAuditSql NVARCHAR(MAX) = N'UPDATE ' + @companyTableName +
                    N' SET ' + @companyAuditSet +
                    N' WHERE ' + QUOTENAME(@companyIdColumn) + N' = @CompanyId;';

                EXEC sp_executesql @companyAuditSql,
                    N'@UserId INT, @CompanyId INT, @Now DATETIME2(0)',
                    @UserId = @userId,
                    @CompanyId = @companyId,
                    @Now = @now;
            END
        END

        SET @step = N'Ensuring Admin role exists';
        PRINT @step;
        IF @rolesCompanyIdExists = 1
        BEGIN
            SELECT TOP (1) @roleId = RoleId FROM dbo.Roles WHERE RoleName = 'Admin' AND CompanyId = @companyId;
        END
        ELSE
        BEGIN
            SELECT TOP (1) @roleId = RoleId FROM dbo.Roles WHERE RoleName = 'Admin';
        END

        IF @roleId IS NULL
        BEGIN
            DECLARE @roleInsertSql NVARCHAR(MAX) =
                N'INSERT INTO dbo.Roles(RoleName' + CASE WHEN @rolesCompanyIdExists = 1 THEN N', CompanyId' ELSE N'' END +
                N') VALUES(@RoleName' + CASE WHEN @rolesCompanyIdExists = 1 THEN N', @CompanyId' ELSE N'' END +
                N'); SELECT @outRoleId = CAST(SCOPE_IDENTITY() AS INT);';

            EXEC sp_executesql @roleInsertSql,
                N'@RoleName NVARCHAR(100), @CompanyId INT, @outRoleId INT OUTPUT',
                @RoleName = N'Admin',
                @CompanyId = @companyId,
                @outRoleId = @roleId OUTPUT;
        END

        SET @step = N'Binding admin user to Admin role';
        PRINT @step;
        IF NOT EXISTS (SELECT 1 FROM dbo.UserRoles WHERE UserId = @userId AND RoleId = @roleId)
        BEGIN
            INSERT INTO dbo.UserRoles(UserId, RoleId) VALUES(@userId, @roleId);
        END

        SET @step = N'Validating company row count';
        PRINT @step;
        DECLARE @companyCount INT;
        EXEC sp_executesql N'SELECT @cnt = COUNT(*) FROM ' + @companyTableName,
            N'@cnt INT OUTPUT',
            @cnt = @companyCount OUTPUT;
        IF (@companyCount <> 1)
            THROW 50005, 'Expected exactly one company record after provisioning.', 1;

        SET @step = N'Validating admin self references';
        PRINT @step;
        IF (@upperLevelExists = 1 OR @upperLevel2Exists = 1 OR @upperLevel3Exists = 1 OR @upperLevel4Exists = 1 OR @upperLevel5Exists = 1 OR
            @insertUserIdExists = 1 OR @updateUserIdExists = 1 OR @createdByExists = 1)
        BEGIN
            IF EXISTS (
                SELECT 1
                FROM dbo.Users
                WHERE UserId = @userId
                  AND ((@upperLevelExists = 1 AND (UpperLevel IS NULL OR UpperLevel <> @userId))
                    OR (@upperLevel2Exists = 1 AND (UpperLevel2 IS NULL OR UpperLevel2 <> @userId))
                    OR (@upperLevel3Exists = 1 AND (UpperLevel3 IS NULL OR UpperLevel3 <> @userId))
                    OR (@upperLevel4Exists = 1 AND (UpperLevel4 IS NULL OR UpperLevel4 <> @userId))
                    OR (@upperLevel5Exists = 1 AND (UpperLevel5 IS NULL OR UpperLevel5 <> @userId))
                    OR (@insertUserIdExists = 1 AND (InsertUserId IS NULL OR InsertUserId <> @userId))
                    OR (@updateUserIdExists = 1 AND (UpdateUserId IS NULL OR UpdateUserId <> @userId))
                    OR (@createdByExists = 1 AND (CreatedBy IS NULL OR CreatedBy <> @userId)))
            )
            BEGIN
                THROW 50006, 'Admin user self-referencing columns were not set correctly.', 1;
            END
        END

        SET @step = N'Finalizing transaction';
        PRINT @step;
        COMMIT;

        SET @step = N'Tenant provisioning completed successfully';
        PRINT @step;
        SELECT 1 AS Success, @userId AS AdminUserId, @companyId AS CompanyId;
    END TRY
    BEGIN CATCH
        DECLARE @errorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @errorNumber INT = ERROR_NUMBER();
        DECLARE @errorSeverity INT = ERROR_SEVERITY();
        DECLARE @errorState INT = ERROR_STATE();
        DECLARE @errorLine INT = ERROR_LINE();

        IF XACT_STATE() <> 0
            ROLLBACK;

        IF @selfConstraintDisabled = 1 AND OBJECT_ID('dbo.Users', 'U') IS NOT NULL
        BEGIN
            BEGIN TRY
                EXEC('ALTER TABLE dbo.Users WITH CHECK CHECK CONSTRAINT FK_UsersUserId_UserId;');
            END TRY
            BEGIN CATCH
                PRINT N'Failed to re-enable FK_UsersUserId_UserId during error handling.';
            END CATCH
        END

        DECLARE @formatted NVARCHAR(4000) = FORMATMESSAGE(N'Tenant provisioning failed at step "%s". Error %d, Line %d: %s',
            ISNULL(@step, N''), @errorNumber, @errorLine, @errorMessage);
        THROW @errorNumber, @formatted, @errorState;
    END CATCH
END
GO
