﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiAlamedas.Db.Models.Alamedas.Models
{
    public partial class alamedascontext : DbContext
    {
        public alamedascontext()
        {
        }

        public alamedascontext(DbContextOptions<alamedascontext> options)
            : base(options)
        {
        }

        public virtual DbSet<Condomino> Condominos { get; set; }
        public virtual DbSet<DetalleGasto> DetalleGastos { get; set; }
        public virtual DbSet<DetalleGastoCajachica> DetalleGastoCajachicas { get; set; }
        public virtual DbSet<DetalleIngreso> DetalleIngresos { get; set; }
        public virtual DbSet<DetalleIngresoCajachica> DetalleIngresoCajachicas { get; set; }
        public virtual DbSet<Error> Errors { get; set; }
        public virtual DbSet<Gasto> Gastos { get; set; }
        public virtual DbSet<GastosCajaChica> GastosCajaChicas { get; set; }
        public virtual DbSet<Ingreso> Ingresos { get; set; }
        public virtual DbSet<Mora> Moras { get; set; }
        public virtual DbSet<ProductoGasto> ProductoGastos { get; set; }
        public virtual DbSet<ProductoGastoCajaChica> ProductoGastoCajaChicas { get; set; }
        public virtual DbSet<ProductoIngresoCajaChica> ProductoIngresoCajaChicas { get; set; }
        public virtual DbSet<TblGastoCajaChica> TblGastoCajaChicas { get; set; }
        public virtual DbSet<TblIngresosCajaChica> TblIngresosCajaChicas { get; set; }
        public virtual DbSet<TblRole> TblRoles { get; set; }
        public virtual DbSet<TblUsuario> TblUsuarios { get; set; }
        public virtual DbSet<TipoGasto> TipoGastos { get; set; }
        public virtual DbSet<TipoIngreso> TipoIngresos { get; set; }
        public virtual DbSet<TipoIngresoCajaChica> TipoIngresoCajaChicas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-JFGLEU7;Initial Catalog=ALAMEDASBD;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Condomino>(entity =>
            {
                entity.HasKey(e => e.IdCondomino)
                    .HasName("PK__CONDOMIN__E3A98DD3C9C7CCCC");

                entity.ToTable("CONDOMINO");

                entity.Property(e => e.IdCondomino)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_CONDOMINO");

                entity.Property(e => e.Activo)
                    .HasColumnName("ACTIVO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CORREO");

                entity.Property(e => e.NombreCompleto)
                    .IsRequired()
                    .HasMaxLength(252)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_COMPLETO");

                entity.Property(e => e.NombreInquilino)
                    .IsRequired()
                    .HasMaxLength(252)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_INQUILINO");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO");
            });

            modelBuilder.Entity<DetalleGasto>(entity =>
            {
                entity.HasKey(e => new { e.Consecutivo, e.IdEntity });

                entity.ToTable("DETALLE_GASTO");

                entity.Property(e => e.Consecutivo).HasColumnName("CONSECUTIVO");

                entity.Property(e => e.IdEntity).HasColumnName("ID_ENTITY");

                entity.Property(e => e.Concepto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO");

                entity.Property(e => e.Valor).HasColumnName("VALOR");

                entity.HasOne(d => d.ConsecutivoNavigation)
                    .WithMany(p => p.DetalleGastos)
                    .HasForeignKey(d => d.Consecutivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DETALLE_GASTO");

                entity.HasOne(d => d.IdEntityNavigation)
                    .WithMany(p => p.DetalleGastos)
                    .HasForeignKey(d => d.IdEntity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DETALLE_PRODUCTO");
            });

            modelBuilder.Entity<DetalleGastoCajachica>(entity =>
            {
                entity.HasKey(e => new { e.Consecutivo, e.IdProdgasto })
                    .HasName("PKCONSECUTIVO_ID_GCCG");

                entity.ToTable("DETALLE_GASTO_CAJACHICA");

                entity.Property(e => e.Consecutivo).HasColumnName("CONSECUTIVO");

                entity.Property(e => e.IdProdgasto).HasColumnName("ID_PRODGASTO");

                entity.Property(e => e.Anio).HasColumnName("ANIO");

                entity.Property(e => e.Concepto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("FECHA");

                entity.Property(e => e.Mes).HasColumnName("MES");

                entity.Property(e => e.Total).HasColumnName("TOTAL");

                entity.HasOne(d => d.ConsecutivoNavigation)
                    .WithMany(p => p.DetalleGastoCajachicas)
                    .HasForeignKey(d => d.Consecutivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONSECUTIVO_GCC");

                entity.HasOne(d => d.IdProdgastoNavigation)
                    .WithMany(p => p.DetalleGastoCajachicas)
                    .HasForeignKey(d => d.IdProdgasto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_PRODGASTO_CCHICA");
            });

            modelBuilder.Entity<DetalleIngreso>(entity =>
            {
                entity.HasKey(e => new { e.Consecutivo, e.IdMora })
                    .HasName("PK_MORA_FACTURA");

                entity.ToTable("DETALLE_INGRESO");

                entity.Property(e => e.Consecutivo).HasColumnName("CONSECUTIVO");

                entity.Property(e => e.IdMora).HasColumnName("ID_MORA");

                entity.Property(e => e.Anio)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("ANIO");

                entity.Property(e => e.Concepto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO");

                entity.Property(e => e.DiasVencido).HasColumnName("DIAS_VENCIDO");

                entity.Property(e => e.Mes)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("MES");

                entity.Property(e => e.Valor).HasColumnName("VALOR");

                entity.HasOne(d => d.ConsecutivoNavigation)
                    .WithMany(p => p.DetalleIngresos)
                    .HasForeignKey(d => d.Consecutivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FACTURA_DETALLE");

                entity.HasOne(d => d.IdMoraNavigation)
                    .WithMany(p => p.DetalleIngresos)
                    .HasForeignKey(d => d.IdMora)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MORA_DETALLE");
            });

            modelBuilder.Entity<DetalleIngresoCajachica>(entity =>
            {
                entity.HasKey(e => new { e.Consecutivo, e.IdProdgasto })
                    .HasName("PKCONSECUTIVO_ID_CCG");

                entity.ToTable("DETALLE_INGRESO_CAJACHICA");

                entity.Property(e => e.Consecutivo).HasColumnName("CONSECUTIVO");

                entity.Property(e => e.IdProdgasto).HasColumnName("ID_PRODGASTO");

                entity.Property(e => e.Anio).HasColumnName("ANIO");

                entity.Property(e => e.Concepto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("FECHA");

                entity.Property(e => e.Mes).HasColumnName("MES");

                entity.Property(e => e.Total).HasColumnName("TOTAL");

                entity.HasOne(d => d.ConsecutivoNavigation)
                    .WithMany(p => p.DetalleIngresoCajachicas)
                    .HasForeignKey(d => d.Consecutivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONSECUTIVO");

                entity.HasOne(d => d.IdProdgastoNavigation)
                    .WithMany(p => p.DetalleIngresoCajachicas)
                    .HasForeignKey(d => d.IdProdgasto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_PRODGASTO");
            });

            modelBuilder.Entity<Error>(entity =>
            {
                entity.HasKey(e => e.IdError)
                    .HasName("PK__ERROR__9046136ABAED8800");

                entity.ToTable("ERROR");

                entity.Property(e => e.IdError).HasColumnName("ID_ERROR");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.Pantalla)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("PANTALLA");
            });

            modelBuilder.Entity<Gasto>(entity =>
            {
                entity.HasKey(e => e.Consecutivo)
                    .HasName("PK__GASTOS__F8CAE286FB46CE27");

                entity.ToTable("GASTOS");

                entity.Property(e => e.Consecutivo)
                    .ValueGeneratedNever()
                    .HasColumnName("CONSECUTIVO");

                entity.Property(e => e.Anio).HasColumnName("ANIO");

                entity.Property(e => e.Concepto)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("FECHA");

                entity.Property(e => e.Gasto1).HasColumnName("GASTO");

                entity.Property(e => e.Mes).HasColumnName("MES");

                entity.Property(e => e.Usuario).HasColumnName("USUARIO");

                entity.Property(e => e.Valor)
                    .HasColumnType("money")
                    .HasColumnName("VALOR");

                entity.HasOne(d => d.Gasto1Navigation)
                    .WithMany(p => p.Gastos)
                    .HasForeignKey(d => d.Gasto1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INGRESOS_TIPOG");

                entity.HasOne(d => d.UsuarioNavigation)
                    .WithMany(p => p.Gastos)
                    .HasForeignKey(d => d.Usuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GASTOS_USUARIO");
            });

            modelBuilder.Entity<GastosCajaChica>(entity =>
            {
                entity.HasKey(e => e.Consecutivo)
                    .HasName("PK__GASTOS_C__F8CAE2860AB91727");

                entity.ToTable("GASTOS_CAJA_CHICA");

                entity.Property(e => e.Consecutivo).HasColumnName("CONSECUTIVO");

                entity.Property(e => e.Anio).HasColumnName("ANIO");

                entity.Property(e => e.Anulado)
                    .HasColumnName("ANULADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Concepto)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("FECHA");

                entity.Property(e => e.Mes).HasColumnName("MES");

                entity.Property(e => e.TipoGastoCchica).HasColumnName("TIPO_GASTO_CCHICA");

                entity.Property(e => e.Total).HasColumnName("TOTAL");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.GastosCajaChicas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO_G_CAJA_CHICA");

                entity.HasOne(d => d.TipoGastoCchicaNavigation)
                    .WithMany(p => p.GastosCajaChicas)
                    .HasForeignKey(d => d.TipoGastoCchica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TIPO_GASTO_CCHICA");
            });

            modelBuilder.Entity<Ingreso>(entity =>
            {
                entity.HasKey(e => e.Consecutivo)
                    .HasName("PK__INGRESOS__F8CAE2868844B769");

                entity.ToTable("INGRESOS");

                entity.Property(e => e.Consecutivo)
                    .ValueGeneratedNever()
                    .HasColumnName("CONSECUTIVO");

                entity.Property(e => e.Anio).HasColumnName("ANIO");

                entity.Property(e => e.Anulado)
                    .HasColumnName("ANULADO")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Concepto)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("FECHA");

                entity.Property(e => e.Ingreso1).HasColumnName("INGRESO");

                entity.Property(e => e.Mes).HasColumnName("MES");

                entity.Property(e => e.NombreInquilino)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_INQUILINO");

                entity.Property(e => e.Total).HasColumnName("TOTAL");

                entity.Property(e => e.Usuario).HasColumnName("USUARIO");

                entity.HasOne(d => d.Ingreso1Navigation)
                    .WithMany(p => p.Ingresos)
                    .HasForeignKey(d => d.Ingreso1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INGRESOS_TIPO");

                entity.HasOne(d => d.UsuarioNavigation)
                    .WithMany(p => p.Ingresos)
                    .HasForeignKey(d => d.Usuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INGRESOS_USUARIO");
            });

            modelBuilder.Entity<Mora>(entity =>
            {
                entity.HasKey(e => e.IdMora)
                    .HasName("PK__MORA__422604A1D11DEE08");

                entity.ToTable("MORA");

                entity.Property(e => e.IdMora).HasColumnName("ID_MORA");

                entity.Property(e => e.Anio)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("ANIO");

                entity.Property(e => e.Concepto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO");

                entity.Property(e => e.Condomino).HasColumnName("CONDOMINO");

                entity.Property(e => e.DiasVencido).HasColumnName("DIAS_VENCIDO");

                entity.Property(e => e.Estado)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("FECHA");

                entity.Property(e => e.Mes)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("MES");

                entity.Property(e => e.Valor)
                    .HasColumnType("money")
                    .HasColumnName("VALOR");

                entity.HasOne(d => d.CondominoNavigation)
                    .WithMany(p => p.Moras)
                    .HasForeignKey(d => d.Condomino)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONDOMINO_MORA");
            });

            modelBuilder.Entity<ProductoGasto>(entity =>
            {
                entity.HasKey(e => e.IdEntity)
                    .HasName("PK__PRODUCTO__06B12ED64D498AD9");

                entity.ToTable("PRODUCTO_GASTO");

                entity.Property(e => e.IdEntity).HasColumnName("ID_ENTITY");

                entity.Property(e => e.Concepto)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO");

                entity.Property(e => e.Valor).HasColumnName("VALOR");
            });

            modelBuilder.Entity<ProductoGastoCajaChica>(entity =>
            {
                entity.ToTable("PRODUCTO_GASTO_CAJA_CHICA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Concepto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO");

                entity.Property(e => e.Valor).HasColumnName("VALOR");
            });

            modelBuilder.Entity<ProductoIngresoCajaChica>(entity =>
            {
                entity.ToTable("PRODUCTO_INGRESO_CAJA_CHICA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Concepto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO");

                entity.Property(e => e.Valor).HasColumnName("VALOR");
            });

            modelBuilder.Entity<TblGastoCajaChica>(entity =>
            {
                entity.HasKey(e => e.IdGastoCajaChica)
                    .HasName("PK__TBL_GAST__26FF85DC26A647CE");

                entity.ToTable("TBL_GASTO_CAJA_CHICA");

                entity.Property(e => e.IdGastoCajaChica).HasColumnName("ID_GASTO_CAJA_CHICA");

                entity.Property(e => e.Activo)
                    .HasColumnName("ACTIVO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombreGastoCajachica)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_GASTO_CAJACHICA");
            });

            modelBuilder.Entity<TblIngresosCajaChica>(entity =>
            {
                entity.HasKey(e => e.Consecutivo)
                    .HasName("PK__TBL_INGR__F8CAE2861555FB48");

                entity.ToTable("TBL_INGRESOS_CAJA_CHICA");

                entity.Property(e => e.Consecutivo).HasColumnName("CONSECUTIVO");

                entity.Property(e => e.Anio).HasColumnName("ANIO");

                entity.Property(e => e.Anulado)
                    .HasColumnName("ANULADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Concepto)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("FECHA");

                entity.Property(e => e.Mes).HasColumnName("MES");

                entity.Property(e => e.TipoIngresoC).HasColumnName("Tipo_IngresoC");

                entity.Property(e => e.Total).HasColumnName("TOTAL");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TblIngresosCajaChicas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO_I_CAJA_CHICA");

                entity.HasOne(d => d.TipoIngresoCNavigation)
                    .WithMany(p => p.TblIngresosCajaChicas)
                    .HasForeignKey(d => d.TipoIngresoC)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TIPO_INGRESO_CCHICA");
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__tblRoles__2A49584C5FCD2CC7");

                entity.ToTable("tblRoles");

                entity.Property(e => e.Descripcion).HasMaxLength(250);

                entity.Property(e => e.Nombre).HasMaxLength(250);
            });

            modelBuilder.Entity<TblUsuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK_Usuarios");

                entity.ToTable("tblUsuarios");

                entity.HasIndex(e => e.Ulogin, "IX_Usuarios")
                    .IsUnique();

                entity.Property(e => e.Correro).HasMaxLength(256);

                entity.Property(e => e.Ulogin)
                    .HasMaxLength(50)
                    .HasColumnName("ULogin");

                entity.Property(e => e.Unombre)
                    .HasMaxLength(256)
                    .HasColumnName("UNombre");
            });

            modelBuilder.Entity<TipoGasto>(entity =>
            {
                entity.HasKey(e => e.IdGasto)
                    .HasName("PK__TIPO_GAS__45B01B7E0DDF3800");

                entity.ToTable("TIPO_GASTO");

                entity.Property(e => e.IdGasto).HasColumnName("ID_GASTO");

                entity.Property(e => e.Activo)
                    .HasColumnName("ACTIVO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombreGasto)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_GASTO");
            });

            modelBuilder.Entity<TipoIngreso>(entity =>
            {
                entity.HasKey(e => e.IdIngreso)
                    .HasName("PK__TIPO_ING__627D3FC4E126E312");

                entity.ToTable("TIPO_INGRESO");

                entity.Property(e => e.IdIngreso).HasColumnName("ID_INGRESO");

                entity.Property(e => e.Activo)
                    .HasColumnName("ACTIVO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombreIngreso)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_INGRESO");
            });

            modelBuilder.Entity<TipoIngresoCajaChica>(entity =>
            {
                entity.HasKey(e => e.IdIngresoaCajaChica)
                    .HasName("PK__TIPO_ING__B32819B6F97C1B35");

                entity.ToTable("TIPO_INGRESO_CAJA_CHICA");

                entity.Property(e => e.IdIngresoaCajaChica).HasColumnName("ID_INGRESOA_CAJA_CHICA");

                entity.Property(e => e.Activo)
                    .HasColumnName("ACTIVO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombreIngresoCajaChica)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_INGRESO_CAJA_CHICA");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__USUARIO__91136B90FE2EC46A");

                entity.ToTable("USUARIO");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("CLAVE");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
