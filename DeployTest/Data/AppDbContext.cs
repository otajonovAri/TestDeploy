﻿using DeployTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DeployTest.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}
