﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using celcoin.Data;

namespace celcoin.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("Relational:Sequence:.Id", "'Id', '', '1000', '1', '', '', 'Int32', 'False'");

            modelBuilder.Entity("celcoin.Models.MeioPagamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<int>("TaxaId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TaxaId");

                    b.ToTable("MeiosPagamento");

                    b.HasData(
                        new
                        {
                            Id = 1000,
                            Nome = "Débito",
                            TaxaId = 1000
                        },
                        new
                        {
                            Id = 1001,
                            Nome = "Crédito",
                            TaxaId = 1001
                        });
                });

            modelBuilder.Entity("celcoin.Models.Taxa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Taxas");

                    b.HasData(
                        new
                        {
                            Id = 1000,
                            Nome = "taxa_debito",
                            Valor = 2.3m
                        },
                        new
                        {
                            Id = 1001,
                            Nome = "taxa_credito",
                            Valor = 4.55m
                        },
                        new
                        {
                            Id = 1002,
                            Nome = "taxa_parcela_credito",
                            Valor = 1.6m
                        },
                        new
                        {
                            Id = 1003,
                            Nome = "taxa_parcela_debito",
                            Valor = 0m
                        });
                });

            modelBuilder.Entity("celcoin.Models.TipoVenda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("TiposVenda");

                    b.HasData(
                        new
                        {
                            Id = 1000,
                            Nome = "CUSTO_VENDEDOR"
                        },
                        new
                        {
                            Id = 1001,
                            Nome = "PARCELADO_CLIENTE"
                        },
                        new
                        {
                            Id = 1002,
                            Nome = "CUSTO_CLIENTE"
                        });
                });

            modelBuilder.Entity("celcoin.Models.Venda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Data")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MeioPagamentoId")
                        .HasColumnType("integer");

                    b.Property<int>("NumParcelas")
                        .HasColumnType("integer");

                    b.Property<decimal>("Recebivel")
                        .HasColumnType("numeric");

                    b.Property<int>("TaxaParcelaId")
                        .HasColumnType("integer");

                    b.Property<int>("TipoVendaId")
                        .HasColumnType("integer");

                    b.Property<decimal>("ValorVenda")
                        .HasColumnType("numeric");

                    b.Property<int>("VendedorId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MeioPagamentoId");

                    b.HasIndex("TaxaParcelaId");

                    b.HasIndex("TipoVendaId");

                    b.HasIndex("VendedorId");

                    b.ToTable("Vendas");

                    b.HasData(
                        new
                        {
                            Id = 1000,
                            Data = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MeioPagamentoId = 1000,
                            NumParcelas = 1,
                            Recebivel = 102.3m,
                            TaxaParcelaId = 1000,
                            TipoVendaId = 1000,
                            ValorVenda = 100m,
                            VendedorId = 1000
                        },
                        new
                        {
                            Id = 1001,
                            Data = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MeioPagamentoId = 1001,
                            NumParcelas = 3,
                            Recebivel = 101.67m,
                            TaxaParcelaId = 1002,
                            TipoVendaId = 1001,
                            ValorVenda = 100m,
                            VendedorId = 1000
                        },
                        new
                        {
                            Id = 1002,
                            Data = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MeioPagamentoId = 1001,
                            NumParcelas = 4,
                            Recebivel = 106.22m,
                            TaxaParcelaId = 1002,
                            TipoVendaId = 1002,
                            ValorVenda = 100m,
                            VendedorId = 1000
                        });
                });

            modelBuilder.Entity("celcoin.Models.Vendedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<double>("Saldo")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Vendedores");

                    b.HasData(
                        new
                        {
                            Id = 1000,
                            Nome = "Marcos Vinícios de Oliveira",
                            Saldo = 0.0
                        });
                });

            modelBuilder.Entity("celcoin.Models.MeioPagamento", b =>
                {
                    b.HasOne("celcoin.Models.Taxa", "Taxa")
                        .WithMany()
                        .HasForeignKey("TaxaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("celcoin.Models.Venda", b =>
                {
                    b.HasOne("celcoin.Models.MeioPagamento", "MeioPagamento")
                        .WithMany()
                        .HasForeignKey("MeioPagamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("celcoin.Models.Taxa", "TaxaParcela")
                        .WithMany()
                        .HasForeignKey("TaxaParcelaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("celcoin.Models.TipoVenda", "TipoVenda")
                        .WithMany()
                        .HasForeignKey("TipoVendaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("celcoin.Models.Vendedor", "Vendedor")
                        .WithMany()
                        .HasForeignKey("VendedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
