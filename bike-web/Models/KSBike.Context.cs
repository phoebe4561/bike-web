﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class KSBikeEntities : DbContext
{
    public KSBikeEntities()
        : base("name=KSBikeEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<hashtag> hashtags { get; set; }

    public virtual DbSet<Home> Homes { get; set; }

    public virtual DbSet<official_route_comment> official_route_comment { get; set; }

    public virtual DbSet<official_route_data> official_route_data { get; set; }

    public virtual DbSet<order> orders { get; set; }

    public virtual DbSet<private_route> private_route { get; set; }

    public virtual DbSet<private_route_comment> private_route_comment { get; set; }

    public virtual DbSet<product> products { get; set; }

    public virtual DbSet<user_favorite> user_favorite { get; set; }

    public virtual DbSet<user> users { get; set; }

}

