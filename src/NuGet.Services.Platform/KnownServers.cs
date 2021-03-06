﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Services
{
    public enum KnownSqlConnection
    {
        /// <summary>
        /// The primary SQL Server for APIv3 Data
        /// </summary>
        Primary,

        /// <summary>
        /// The SQL Server for Legacy APIv2 Data
        /// </summary>
        Legacy,

        /// <summary>
        /// The SQL Server for Warehouse data
        /// </summary>
        Warehouse
    }

    public enum KnownStorageAccount
    {
        Primary,
        Legacy,
        Backup
    }
}
