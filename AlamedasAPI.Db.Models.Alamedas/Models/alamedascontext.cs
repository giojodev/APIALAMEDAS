using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AlamedasAPI.Db.Models.Alamedas.Models
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

        public virtual DbSet<ApiLog> ApiLogs { get; set; } = null!;
        public virtual DbSet<Condomino> Condominos { get; set; } = null!;
        public virtual DbSet<DetalleGasto> DetalleGastos { get; set; } = null!;
        public virtual DbSet<DetalleGastoCajachica> DetalleGastoCajachicas { get; set; } = null!;
        public virtual DbSet<DetalleIngreso> DetalleIngresos { get; set; } = null!;
        public virtual DbSet<DetalleIngresoCajachica> DetalleIngresoCajachicas { get; set; } = null!;
        public virtual DbSet<Gasto> Gastos { get; set; } = null!;
        public virtual DbSet<Ingreso> Ingresos { get; set; } = null!;
        public virtual DbSet<Mora> Moras { get; set; } = null!;
        public virtual DbSet<MovimientosDoc> MovimientosDocs { get; set; } = null!;
        public virtual DbSet<ProductoGasto> ProductoGastos { get; set; } = null!;
        public virtual DbSet<ProductoGastoCajaChica> ProductoGastoCajaChicas { get; set; } = null!;
        public virtual DbSet<ProductoIngresoCajaChica> ProductoIngresoCajaChicas { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<TblGastosCajaChica> TblGastosCajaChicas { get; set; } = null!;
        public virtual DbSet<TblIngresosCajaChica> TblIngresosCajaChicas { get; set; } = null!;
        public virtual DbSet<TipoGasto> TipoGastos { get; set; } = null!;
        public virtual DbSet<TipoGastoCajaChica> TipoGastoCajaChicas { get; set; } = null!;
        public virtual DbSet<TipoIngreso> TipoIngresos { get; set; } = null!;
        public virtual DbSet<TipoIngresoCajaChica> TipoIngresoCajaChicas { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer("Data Source=DESKTOP-JFGLEU7;Initial Catalog=ALAMEDASBD;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApiLog>(entity =>
            {
                entity.ToTable("ApiLog");

                entity.Property(e => e.Level).HasMaxLength(50);

                entity.Property(e => e.Logged).HasColumnType("datetime");

                entity.Property(e => e.Logger).HasMaxLength(250);

                entity.Property(e => e.MachineName).HasMaxLength(50);
            });

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
                    .HasMaxLength(252)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_COMPLETO");

                entity.Property(e => e.NombreInquilino)
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
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO");

                entity.Property(e => e.Valor).HasColumnName("VALOR");

                /*entity.HasOne(d => d.ConsecutivoNavigation)
                    .WithMany(p => p.DetalleGastos)
                    .HasForeignKey(d => d.Consecutivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DETALLE_GASTO");

                entity.HasOne(d => d.IdEntityNavigation)
                    .WithMany(p => p.DetalleGastos)
                    .HasForeignKey(d => d.IdEntity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DETALLE_PRODUCTO");*/
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
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("FECHA");

                entity.Property(e => e.Mes).HasColumnName("MES");

                entity.Property(e => e.Total).HasColumnName("TOTAL");

                /*entity.HasOne(d => d.ConsecutivoNavigation)
                    .WithMany(p => p.DetalleGastoCajachicas)
                    .HasForeignKey(d => d.Consecutivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONSECUTIVO_GCC");

                entity.HasOne(d => d.IdProdgastoNavigation)
                    .WithMany(p => p.DetalleGastoCajachicas)
                    .HasForeignKey(d => d.IdProdgasto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_PRODGASTO_CCHICA");*/
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

                /*entity.HasOne(d => d.ConsecutivoNavigation)
                    .WithMany(p => p.DetalleIngresos)
                    .HasForeignKey(d => d.Consecutivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FACTURA_DETALLE");

                entity.HasOne(d => d.IdMoraNavigation)
                    .WithMany(p => p.DetalleIngresos)
                    .HasForeignKey(d => d.IdMora)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MORA_DETALLE");*/
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
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("FECHA");

                entity.Property(e => e.Mes).HasColumnName("MES");

                entity.Property(e => e.Total).HasColumnName("TOTAL");

                /*entity.HasOne(d => d.ConsecutivoNavigation)
                    .WithMany(p => p.DetalleIngresoCajachicas)
                    .HasForeignKey(d => d.Consecutivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONSECUTIVO");

                entity.HasOne(d => d.IdProdgastoNavigation)
                    .WithMany(p => p.DetalleIngresoCajachicas)
                    .HasForeignKey(d => d.IdProdgasto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_PRODGASTO");*/
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

                entity.Property(e => e.Idusuario).HasColumnName("IDUSUARIO");

                entity.Property(e => e.Mes).HasColumnName("MES");

                entity.Property(e => e.Valor)
                    .HasColumnType("money")
                    .HasColumnName("VALOR");

               /* entity.HasOne(d => d.Gasto1Navigation)
                    .WithMany(p => p.Gastos)
                    .HasForeignKey(d => d.Gasto1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INGRESOS_TIPOG");

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.Gastos)
                    .HasForeignKey(d => d.Idusuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GASTOS_USUARIO");*/
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

                entity.Property(e => e.Idusuario).HasColumnName("IDUSUARIO");

                entity.Property(e => e.Ingreso1).HasColumnName("INGRESO");

                entity.Property(e => e.Mes).HasColumnName("MES");

                entity.Property(e => e.NombreInquilino)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_INQUILINO");

                entity.Property(e => e.Total).HasColumnName("TOTAL");

               /* entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.Ingresos)
                    .HasForeignKey(d => d.Idusuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INGRESOS_USUARIO");

                entity.HasOne(d => d.Ingreso1Navigation)
                    .WithMany(p => p.Ingresos)
                    .HasForeignKey(d => d.Ingreso1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_INGRESOS_TIPO");*/
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

                /*entity.HasOne(d => d.CondominoNavigation)
                    .WithMany(p => p.Moras)
                    .HasForeignKey(d => d.Condomino)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONDOMINO_MORA");*/
            });

            modelBuilder.Entity<MovimientosDoc>(entity =>
            {
                entity.HasKey(e => e.IdMovimiento);

                entity.ToTable("MOVIMIENTOS_DOC");

                entity.Property(e => e.IdMovimiento)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_MOVIMIENTO");

                entity.Property(e => e.Anulado)
                    .HasColumnName("ANULADO")
                    .HasComment("ESTADO DEL MOV ");

                entity.Property(e => e.FechaAnulado)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_ANULADO");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_INGRESO")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdDocumento)
                    .HasColumnName("ID_DOCUMENTO")
                    .HasComment("CONSECUTIVO O LLAVE DEL DOCUMENTO");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("ID_USUARIO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Modulo)
                    .HasMaxLength(50)
                    .HasColumnName("MODULO")
                    .HasDefaultValueSql("(N'FACTURACION')")
                    .HasComment("MODULO QUE DISPARA EL MOVIMIENTO CAJA CHICA / FACTURACION");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(1)
                    .HasColumnName("TIPO")
                    .HasDefaultValueSql("(N'I')")
                    .IsFixedLength()
                    .HasComment("TIPO DE MOVIMIENTO I - INGRESO / G - GASTO");

                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasColumnName("TOTAL");

                /*entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.MovimientosDocs)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MOVIMIENT__IdUsu__503BEA1C");*/
            });

            modelBuilder.Entity<ProductoGasto>(entity =>
            {
                entity.HasKey(e => e.IdEntity)
                    .HasName("PK__PRODUCTO__06B12ED64D498AD9");

                entity.ToTable("PRODUCTO_GASTO");

                entity.Property(e => e.IdEntity).HasColumnName("ID_ENTITY");

                entity.Property(e => e.Concepto)
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
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO");

                entity.Property(e => e.Valor).HasColumnName("VALOR");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__tblRoles__2A49584C5FCD2CC7");

                entity.ToTable("ROLES");

                entity.Property(e => e.IdRol).HasColumnName("ID_ROL");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<TblGastosCajaChica>(entity =>
            {
                entity.HasKey(e => e.Consecutivo)
                    .HasName("PK__GASTOS_C__F8CAE2860AB91727");

                entity.ToTable("TBL_GASTOS_CAJA_CHICA");

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

                /*entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TblGastosCajaChicas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO_G_CAJA_CHICA");

                entity.HasOne(d => d.TipoGastoCchicaNavigation)
                    .WithMany(p => p.TblGastosCajaChicas)
                    .HasForeignKey(d => d.TipoGastoCchica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TIPO_GASTO_CCHICA");*/
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

               /* entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TblIngresosCajaChicas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO_I_CAJA_CHICA");

                entity.HasOne(d => d.TipoIngresoCNavigation)
                    .WithMany(p => p.TblIngresosCajaChicas)
                    .HasForeignKey(d => d.TipoIngresoC)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TIPO_INGRESO_CCHICA");*/
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
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_GASTO");
            });

            modelBuilder.Entity<TipoGastoCajaChica>(entity =>
            {
                entity.HasKey(e => e.IdGastoCajaChica)
                    .HasName("PK__TBL_GAST__26FF85DC26A647CE");

                entity.ToTable("TIPO_GASTO_CAJA_CHICA");

                entity.Property(e => e.IdGastoCajaChica).HasColumnName("ID_GASTO_CAJA_CHICA");

                entity.Property(e => e.Activo)
                    .HasColumnName("ACTIVO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombreGastoCajachica)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_GASTO_CAJACHICA");
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
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_INGRESO_CAJA_CHICA");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK_Usuarios");

                entity.ToTable("USUARIOS");

                entity.HasIndex(e => e.Usuario1, "IX_Usuarios")
                    .IsUnique();

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("CONTRASENA");

                entity.Property(e => e.Correo)
                    .HasMaxLength(256)
                    .HasColumnName("CORREO");

                entity.Property(e => e.IdRol).HasColumnName("ID_ROL");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(256)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(50)
                    .HasColumnName("USUARIO");

                /*entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_tblUsuarios_tblroles");*/
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
