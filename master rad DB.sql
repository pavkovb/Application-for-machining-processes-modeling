USE [master rad2]
GO
/****** Object:  Table [dbo].[Ulazni parametri]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ulazni parametri](
	[UlazniParametar] [nvarchar](50) NOT NULL,
	[MernaJedinica] [nvarchar](50) NOT NULL,
	[Oznaka] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Ulazni parametri] PRIMARY KEY CLUSTERED 
(
	[UlazniParametar] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tolerancije plocica]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tolerancije plocica](
	[Tolerancija] [nvarchar](1) NOT NULL,
 CONSTRAINT [PK_Tolerancije plocica] PRIMARY KEY CLUSTERED 
(
	[Tolerancija] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipovi plocica]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipovi plocica](
	[TipPlocice] [nvarchar](1) NOT NULL,
 CONSTRAINT [PK_Tipovi plocica] PRIMARY KEY CLUSTERED 
(
	[TipPlocice] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipovi obrada]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipovi obrada](
	[TipObrade] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Tipovi obrada] PRIMARY KEY CLUSTERED 
(
	[TipObrade] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Smerovi drzaca alata]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Smerovi drzaca alata](
	[Smer] [nvarchar](1) NOT NULL,
 CONSTRAINT [PK_SmeroviDrzacaAlata] PRIMARY KEY CLUSTERED 
(
	[Smer] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Slike za formu]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Slike za formu](
	[NazivSlike] [varchar](50) NOT NULL,
	[Slika] [varbinary](max) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Preporuke za postojanost alata]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Preporuke za postojanost alata](
	[PostojanostAlata] [float] NOT NULL,
	[KorekcioniFaktor] [float] NOT NULL,
 CONSTRAINT [PK_Postojanosti alata] PRIMARY KEY CLUSTERED 
(
	[PostojanostAlata] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Preporuke za materijal]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Preporuke za materijal](
	[CMC] [float] NOT NULL,
	[Kvalitet] [varchar](50) NOT NULL,
	[V1] [float] NOT NULL,
	[S1] [float] NOT NULL,
	[V2] [float] NOT NULL,
	[S2] [float] NOT NULL,
	[V3] [float] NOT NULL,
	[S3] [float] NOT NULL,
 CONSTRAINT [PK_Preporuke za materijal] PRIMARY KEY CLUSTERED 
(
	[CMC] ASC,
	[Kvalitet] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Kvaliteti obrade]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kvaliteti obrade](
	[Kvalitet] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Kvaliteti Plocica] PRIMARY KEY CLUSTERED 
(
	[Kvalitet] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Precnici pripremaka]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Precnici pripremaka](
	[Precnik] [float] NOT NULL,
 CONSTRAINT [PK_Precnici pripremaka_1] PRIMARY KEY CLUSTERED 
(
	[Precnik] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Precnici obradaka]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Precnici obradaka](
	[PrecinikID] [int] IDENTITY(1,1) NOT NULL,
	[PrecnikMin] [float] NOT NULL,
	[PrecnikMax] [float] NOT NULL,
 CONSTRAINT [PK_Precnici pripremaka] PRIMARY KEY CLUSTERED 
(
	[PrecinikID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Oblici plocica]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Oblici plocica](
	[OblikPlocice] [nvarchar](1) NOT NULL,
 CONSTRAINT [PK_Oblici plocica] PRIMARY KEY CLUSTERED 
(
	[OblikPlocice] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Napadni uglovi]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Napadni uglovi](
	[NapadniUgao] [nvarchar](1) NOT NULL,
	[VrednostNapadnogUgla] [float] NOT NULL,
 CONSTRAINT [PK_NapadniUglovi] PRIMARY KEY CLUSTERED 
(
	[NapadniUgao] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nacini pricvrscavanja plocice]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nacini pricvrscavanja plocice](
	[PricvrscivanjaPlocice] [nvarchar](1) NOT NULL,
 CONSTRAINT [PK_NaciniPricvrscavanjaPlocice] PRIMARY KEY CLUSTERED 
(
	[PricvrscivanjaPlocice] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Materijali]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Materijali](
	[CMC] [float] NOT NULL,
	[Materijal] [nvarchar](255) NOT NULL,
	[Kc1] [float] NOT NULL,
	[HB] [float] NOT NULL,
 CONSTRAINT [PK_Materijali] PRIMARY KEY CLUSTERED 
(
	[CMC] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Masine alatke]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Masine alatke](
	[Masina] [nvarchar](255) NOT NULL,
	[DMax] [float] NOT NULL,
	[DOprimalno] [float] NOT NULL,
	[LMax] [float] NOT NULL,
	[NMin] [float] NOT NULL,
	[NMax] [float] NOT NULL,
	[Pm] [float] NOT NULL,
	[StepenIskoristenja] [float] NOT NULL,
 CONSTRAINT [PK_Masine alatke] PRIMARY KEY CLUSTERED 
(
	[Masina] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ledjni uglovi plocica]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ledjni uglovi plocica](
	[LedjniUgao] [nvarchar](1) NOT NULL,
 CONSTRAINT [PK_Ledjni uglovi plocica] PRIMARY KEY CLUSTERED 
(
	[LedjniUgao] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gradacije obrada]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gradacije obrada](
	[GradacijaObrade] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_GradacijeObrada] PRIMARY KEY CLUSTERED 
(
	[GradacijaObrade] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Izlazni parametri]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Izlazni parametri](
	[IzlazniParametar] [nvarchar](50) NOT NULL,
	[MernaJedinica] [nvarchar](50) NOT NULL,
	[Oznaka] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Izlazni parametri] PRIMARY KEY CLUSTERED 
(
	[IzlazniParametar] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Duzine obradaka]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Duzine obradaka](
	[DuzinaID] [int] IDENTITY(1,1) NOT NULL,
	[DuzinaMin] [float] NOT NULL,
	[DuzinaMax] [float] NOT NULL,
 CONSTRAINT [PK_Duzine pripremaka] PRIMARY KEY CLUSTERED 
(
	[DuzinaID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Duzine alata]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Duzine alata](
	[DuzinaAlata] [nvarchar](1) NOT NULL,
	[VrednostDuzine] [float] NOT NULL,
 CONSTRAINT [PK_DuzineAlata] PRIMARY KEY CLUSTERED 
(
	[DuzinaAlata] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vrste obrada]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vrste obrada](
	[VrstaObrade] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Vrste obrada] PRIMARY KEY CLUSTERED 
(
	[VrstaObrade] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vrste materijala]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vrste materijala](
	[VrstaMaterijala] [nvarchar](50) NOT NULL,
	[NazivMaterijala] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_VrsteMaterijala] PRIMARY KEY CLUSTERED 
(
	[VrstaMaterijala] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pripremci]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pripremci](
	[CMC] [float] NOT NULL,
	[Precnik] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View Materijali]    Script Date: 01/23/2017 05:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View Materijali]
AS
SELECT     dbo.Materijali.CMC, dbo.Materijali.Materijal, dbo.Materijali.Kc1, dbo.Materijali.HB, dbo.[Vrste materijala].VrstaMaterijala
FROM         dbo.Materijali CROSS JOIN
                      dbo.[Vrste materijala]
WHERE     (dbo.[Vrste materijala].VrstaMaterijala = 'P')
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Materijali"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Vrste materijala"
            Begin Extent = 
               Top = 6
               Left = 227
               Bottom = 84
               Right = 380
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View Materijali'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View Materijali'
GO
/****** Object:  Table [dbo].[Drzaci alata]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drzaci alata](
	[DrzacID] [int] IDENTITY(1,1) NOT NULL,
	[PricvrscivanjePlocice] [nvarchar](1) NOT NULL,
	[OblikPlocice] [nvarchar](1) NOT NULL,
	[NapadniUgao] [nvarchar](1) NOT NULL,
	[LedjniUgao] [nvarchar](1) NOT NULL,
	[VisinaDrzaca] [float] NOT NULL,
	[SirinaDrzaca] [float] NOT NULL,
	[DuzinaAlata] [nvarchar](1) NOT NULL,
	[iC] [float] NOT NULL,
	[f1] [float] NOT NULL,
	[h1] [float] NOT NULL,
	[l3] [float] NOT NULL,
	[Gama] [float] NOT NULL,
	[Lambda] [float] NOT NULL,
	[MomentPritezanja] [float] NOT NULL,
 CONSTRAINT [PK_Drzaci alata] PRIMARY KEY CLUSTERED 
(
	[DrzacID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dodaci za uzduznu obradu struganjem]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dodaci za uzduznu obradu struganjem](
	[DuzinaID] [int] NOT NULL,
	[PrecnikID] [int] NOT NULL,
	[DodatakStrGrubo] [float] NOT NULL,
	[DodatakStrFino] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dodaci za spoljasnju kruznu obradu brusenjem]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dodaci za spoljasnju kruznu obradu brusenjem](
	[DuzinaID] [int] NOT NULL,
	[PrecnikID] [int] NOT NULL,
	[DodatakBrMin] [float] NOT NULL,
	[DodatakBrMax] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kvaliteti plocica]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kvaliteti plocica](
	[KvalitetObrade] [nvarchar](3) NOT NULL,
	[GradacijaObrade] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Kvaliteti obrade] PRIMARY KEY CLUSTERED 
(
	[KvalitetObrade] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Obrade]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Obrade](
	[ObradaID] [int] IDENTITY(1,1) NOT NULL,
	[VrstaObrade] [nvarchar](50) NOT NULL,
	[TipObrade] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Obrade] PRIMARY KEY CLUSTERED 
(
	[ObradaID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Klase kvaliteta obradjene povrsine]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Klase kvaliteta obradjene povrsine](
	[KlasaKvaliteta] [nvarchar](3) NOT NULL,
	[RaMax] [float] NOT NULL,
	[GradacijaObrade] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_KlaseKvalitetaObradjenePovrsine] PRIMARY KEY CLUSTERED 
(
	[KlasaKvaliteta] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Slike obrada]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Slike obrada](
	[ObradaID] [int] NOT NULL,
	[SlikaObrade] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Slike obrada] PRIMARY KEY CLUSTERED 
(
	[ObradaID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Rezne plocice]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rezne plocice](
	[PlocicaID] [int] IDENTITY(1,1) NOT NULL,
	[OznakaPlocice]  AS (((((((([OblikPlocice]+[LedjniUgao])+[Tolerancija])+[TipPlocice])+case when len([DuzinaRezneIvice])=(1) then '0'+CONVERT([varchar],[DuzinaRezneIvice],(0)) else CONVERT([varchar],[DuzinaRezneIvice],(0)) end)+case when len([Debljina])=(1) then '0'+CONVERT([varchar],[Debljina],(0)) else CONVERT([varchar],[Debljina],(0)) end)+case when len([Radijus])=(1) then '0'+CONVERT([varchar],[Radijus],(0)) else CONVERT([varchar],[Radijus],(0)) end)+'-')+[KvalitetObrade]) PERSISTED,
	[OblikPlocice] [nvarchar](1) NOT NULL,
	[LedjniUgao] [nvarchar](1) NOT NULL,
	[Tolerancija] [nvarchar](1) NOT NULL,
	[TipPlocice] [nvarchar](1) NOT NULL,
	[DuzinaRezneIvice] [float] NOT NULL,
	[Debljina] [float] NOT NULL,
	[Radijus] [float] NOT NULL,
	[KvalitetObrade] [nvarchar](3) NOT NULL,
 CONSTRAINT [PK_Rezne Plocice_1] PRIMARY KEY CLUSTERED 
(
	[PlocicaID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Kvaliteti obrade u zavisnosti od vrste materijala]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kvaliteti obrade u zavisnosti od vrste materijala](
	[VrstaMaterijala] [nvarchar](50) NOT NULL,
	[KvalitetObrade] [nvarchar](3) NOT NULL,
 CONSTRAINT [PK_Kvaliteti obrade u zavisnosti od vrste materijala] PRIMARY KEY CLUSTERED 
(
	[VrstaMaterijala] ASC,
	[KvalitetObrade] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Izbor oblika plocice]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Izbor oblika plocice](
	[ObradaID] [int] NOT NULL,
	[OblikPlocice] [nvarchar](1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View Drzaci alata]    Script Date: 01/23/2017 05:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View Drzaci alata]
AS
SELECT     dbo.[Drzaci alata].PricvrscivanjePlocice + dbo.[Drzaci alata].OblikPlocice + dbo.[Drzaci alata].NapadniUgao + dbo.[Drzaci alata].LedjniUgao + dbo.[Smerovi drzaca alata].Smer
                       + CASE WHEN len([VisinaDrzaca]) = (1) THEN '0' + CONVERT([varchar], [VisinaDrzaca], (0)) ELSE CONVERT([varchar], [VisinaDrzaca], (0)) 
                      END + CASE WHEN len([SirinaDrzaca]) = (1) THEN '0' + CONVERT([varchar], [SirinaDrzaca], (0)) ELSE CONVERT([varchar], [SirinaDrzaca], (0)) 
                      END + CASE WHEN len([iC]) = (1) THEN '0' + CONVERT([varchar], [iC], (0)) ELSE CONVERT([varchar], [iC], (0)) END AS OznakaDrzaca, 
                      dbo.[Drzaci alata].PricvrscivanjePlocice, dbo.[Drzaci alata].OblikPlocice, dbo.[Drzaci alata].NapadniUgao, dbo.[Drzaci alata].LedjniUgao, 
                      dbo.[Smerovi drzaca alata].Smer, dbo.[Drzaci alata].VisinaDrzaca, dbo.[Drzaci alata].SirinaDrzaca, dbo.[Drzaci alata].DuzinaAlata, dbo.[Drzaci alata].iC, 
                      dbo.[Drzaci alata].f1, dbo.[Drzaci alata].h1, dbo.[Drzaci alata].l3, dbo.[Drzaci alata].Gama, dbo.[Drzaci alata].Lambda, dbo.[Drzaci alata].MomentPritezanja, 
                      dbo.[Duzine alata].VrednostDuzine, dbo.[Napadni uglovi].VrednostNapadnogUgla
FROM         dbo.[Drzaci alata] INNER JOIN
                      dbo.[Duzine alata] ON dbo.[Drzaci alata].DuzinaAlata = dbo.[Duzine alata].DuzinaAlata INNER JOIN
                      dbo.[Napadni uglovi] ON dbo.[Drzaci alata].NapadniUgao = dbo.[Napadni uglovi].NapadniUgao CROSS JOIN
                      dbo.[Smerovi drzaca alata]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Smerovi drzaca alata"
            Begin Extent = 
               Top = 6
               Left = 254
               Bottom = 69
               Right = 405
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Drzaci alata"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 216
            End
            DisplayFlags = 280
            TopColumn = 11
         End
         Begin Table = "Duzine alata"
            Begin Extent = 
               Top = 6
               Left = 443
               Bottom = 84
               Right = 598
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Napadni uglovi"
            Begin Extent = 
               Top = 6
               Left = 636
               Bottom = 84
               Right = 829
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 18
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View Drzaci alata'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View Drzaci alata'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View Drzaci alata'
GO
/****** Object:  View [dbo].[View Dodaci za obradu]    Script Date: 01/23/2017 05:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View Dodaci za obradu]
AS
SELECT     dbo.[Precnici obradaka].PrecnikMin, dbo.[Precnici obradaka].PrecnikMax, dbo.[Duzine obradaka].DuzinaMin, dbo.[Duzine obradaka].DuzinaMax, 
                      dbo.[Dodaci za uzduznu obradu struganjem].DodatakStrGrubo, dbo.[Dodaci za uzduznu obradu struganjem].DodatakStrFino, 
                      dbo.[Dodaci za spoljasnju kruznu obradu brusenjem].DodatakBrMin, dbo.[Dodaci za spoljasnju kruznu obradu brusenjem].DodatakBrMax
FROM         dbo.[Dodaci za spoljasnju kruznu obradu brusenjem] INNER JOIN
                      dbo.[Duzine obradaka] ON dbo.[Dodaci za spoljasnju kruznu obradu brusenjem].DuzinaID = dbo.[Duzine obradaka].DuzinaID INNER JOIN
                      dbo.[Dodaci za uzduznu obradu struganjem] ON dbo.[Duzine obradaka].DuzinaID = dbo.[Dodaci za uzduznu obradu struganjem].DuzinaID INNER JOIN
                      dbo.[Precnici obradaka] ON dbo.[Dodaci za spoljasnju kruznu obradu brusenjem].PrecnikID = dbo.[Precnici obradaka].PrecinikID AND 
                      dbo.[Dodaci za uzduznu obradu struganjem].PrecnikID = dbo.[Precnici obradaka].PrecinikID
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Dodaci za spoljasnju kruznu obradu brusenjem"
            Begin Extent = 
               Top = 83
               Left = 606
               Bottom = 191
               Right = 757
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Dodaci za uzduznu obradu struganjem"
            Begin Extent = 
               Top = 105
               Left = 61
               Bottom = 213
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Duzine obradaka"
            Begin Extent = 
               Top = 47
               Left = 323
               Bottom = 143
               Right = 474
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Precnici obradaka"
            Begin Extent = 
               Top = 181
               Left = 324
               Bottom = 274
               Right = 475
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View Dodaci za obradu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View Dodaci za obradu'
GO
/****** Object:  View [dbo].[View Kvaliteti plocica]    Script Date: 01/23/2017 05:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View Kvaliteti plocica]
AS
SELECT     dbo.[Kvaliteti obrade u zavisnosti od vrste materijala].VrstaMaterijala, dbo.[Kvaliteti obrade u zavisnosti od vrste materijala].KvalitetObrade, 
                      dbo.[Kvaliteti plocica].GradacijaObrade
FROM         dbo.[Kvaliteti plocica] INNER JOIN
                      dbo.[Kvaliteti obrade u zavisnosti od vrste materijala] ON dbo.[Kvaliteti plocica].KvalitetObrade = dbo.[Kvaliteti obrade u zavisnosti od vrste materijala].KvalitetObrade
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Kvaliteti plocica"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 84
               Right = 199
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Kvaliteti obrade u zavisnosti od vrste materijala"
            Begin Extent = 
               Top = 6
               Left = 237
               Bottom = 84
               Right = 389
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View Kvaliteti plocica'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View Kvaliteti plocica'
GO
/****** Object:  View [dbo].[View Obrade]    Script Date: 01/23/2017 05:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View Obrade]
AS
SELECT     dbo.Obrade.VrstaObrade, dbo.Obrade.TipObrade, dbo.[Slike obrada].SlikaObrade
FROM         dbo.Obrade INNER JOIN
                      dbo.[Slike obrada] ON dbo.Obrade.ObradaID = dbo.[Slike obrada].ObradaID
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Obrade"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 99
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Slike obrada"
            Begin Extent = 
               Top = 6
               Left = 227
               Bottom = 84
               Right = 378
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View Obrade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View Obrade'
GO
/****** Object:  View [dbo].[View Oblici plocica]    Script Date: 01/23/2017 05:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View Oblici plocica]
AS
SELECT     dbo.Obrade.VrstaObrade, dbo.Obrade.TipObrade, dbo.[Izbor oblika plocice].OblikPlocice
FROM         dbo.[Izbor oblika plocice] INNER JOIN
                      dbo.[Oblici plocica] ON dbo.[Izbor oblika plocice].OblikPlocice = dbo.[Oblici plocica].OblikPlocice INNER JOIN
                      dbo.Obrade ON dbo.[Izbor oblika plocice].ObradaID = dbo.Obrade.ObradaID INNER JOIN
                      dbo.[Tipovi obrada] ON dbo.Obrade.TipObrade = dbo.[Tipovi obrada].TipObrade INNER JOIN
                      dbo.[Vrste obrada] ON dbo.Obrade.VrstaObrade = dbo.[Vrste obrada].VrstaObrade
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Izbor oblika plocice"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 84
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Oblici plocica"
            Begin Extent = 
               Top = 6
               Left = 227
               Bottom = 69
               Right = 378
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Obrade"
            Begin Extent = 
               Top = 72
               Left = 227
               Bottom = 165
               Right = 378
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tipovi obrada"
            Begin Extent = 
               Top = 84
               Left = 38
               Bottom = 147
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Vrste obrada"
            Begin Extent = 
               Top = 150
               Left = 38
               Bottom = 213
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View Oblici plocica'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'= 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View Oblici plocica'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View Oblici plocica'
GO
/****** Object:  Table [dbo].[Eksperimenti]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Eksperimenti](
	[NazivEksperimenta] [nvarchar](255) NOT NULL,
	[VremeIzvodjenjaEksperimenta] [datetime] NOT NULL,
	[PlocicaID] [int] NOT NULL,
	[CMC] [float] NOT NULL,
	[UlazniParametar] [nvarchar](50) NOT NULL,
	[IzlazniParametar] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Eksperimenti] PRIMARY KEY CLUSTERED 
(
	[NazivEksperimenta] ASC,
	[VremeIzvodjenjaEksperimenta] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Preporuceni rezimi rezanja]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Preporuceni rezimi rezanja](
	[Kvalitet] [nvarchar](10) NOT NULL,
	[PlocicaID] [int] NOT NULL,
	[APreporuceno] [float] NOT NULL,
	[AMin] [float] NOT NULL,
	[AMax] [float] NOT NULL,
	[SPreporuceno] [float] NOT NULL,
	[SMin] [float] NOT NULL,
	[SMax] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Izmerene vrednosti eksperimenata]    Script Date: 01/23/2017 05:52:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Izmerene vrednosti eksperimenata](
	[NazivEksperimenta] [nvarchar](255) NOT NULL,
	[VremeIzvodjenjaEksperimenta] [datetime] NOT NULL,
	[VremenskiInterval] [float] NOT NULL,
	[VrednostIzlaznogParametra] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View Preporuceni rezimi]    Script Date: 01/23/2017 05:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View Preporuceni rezimi]
AS
SELECT     TOP (100) PERCENT dbo.[Preporuceni rezimi rezanja].Kvalitet, dbo.[Rezne plocice].OznakaPlocice, dbo.[Rezne plocice].OblikPlocice, dbo.[Rezne plocice].LedjniUgao, 
                      dbo.[Rezne plocice].Tolerancija, dbo.[Rezne plocice].TipPlocice, dbo.[Rezne plocice].DuzinaRezneIvice, dbo.[Rezne plocice].Debljina, dbo.[Rezne plocice].Radijus, 
                      dbo.[Rezne plocice].KvalitetObrade, dbo.[Preporuceni rezimi rezanja].APreporuceno, dbo.[Preporuceni rezimi rezanja].AMin, dbo.[Preporuceni rezimi rezanja].AMax, 
                      dbo.[Preporuceni rezimi rezanja].SPreporuceno, dbo.[Preporuceni rezimi rezanja].SMin, dbo.[Preporuceni rezimi rezanja].SMax
FROM         dbo.[Preporuceni rezimi rezanja] INNER JOIN
                      dbo.[Rezne plocice] ON dbo.[Preporuceni rezimi rezanja].PlocicaID = dbo.[Rezne plocice].PlocicaID
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Preporuceni rezimi rezanja"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Rezne plocice"
            Begin Extent = 
               Top = 6
               Left = 227
               Bottom = 114
               Right = 392
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1785
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View Preporuceni rezimi'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View Preporuceni rezimi'
GO
/****** Object:  View [dbo].[View_1]    Script Date: 01/23/2017 05:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_1]
AS
SELECT     dbo.[Preporuke za materijal].CMC, dbo.[View Preporuceni rezimi].Kvalitet, dbo.[View Preporuceni rezimi].OznakaPlocice, dbo.[View Preporuceni rezimi].OblikPlocice, 
                      dbo.[View Preporuceni rezimi].LedjniUgao, dbo.[View Preporuceni rezimi].Tolerancija, dbo.[View Preporuceni rezimi].TipPlocice, 
                      dbo.[View Preporuceni rezimi].DuzinaRezneIvice, dbo.[View Preporuceni rezimi].Debljina, dbo.[View Preporuceni rezimi].Radijus, 
                      dbo.[View Preporuceni rezimi].KvalitetObrade, dbo.[View Preporuceni rezimi].APreporuceno, dbo.[View Preporuceni rezimi].AMin, dbo.[View Preporuceni rezimi].AMax, 
                      dbo.[View Preporuceni rezimi].SPreporuceno, dbo.[View Preporuceni rezimi].SMin, dbo.[View Preporuceni rezimi].SMax, dbo.[Preporuke za materijal].V1, 
                      dbo.[Preporuke za materijal].S1, dbo.[Preporuke za materijal].V2, dbo.[Preporuke za materijal].S2, dbo.[Preporuke za materijal].V3, 
                      dbo.[Preporuke za materijal].S3
FROM         dbo.[View Preporuceni rezimi] INNER JOIN
                      dbo.[Preporuke za materijal] ON dbo.[View Preporuceni rezimi].Kvalitet = dbo.[Preporuke za materijal].Kvalitet
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "View Preporuceni rezimi"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 203
            End
            DisplayFlags = 280
            TopColumn = 12
         End
         Begin Table = "Preporuke za materijal"
            Begin Extent = 
               Top = 6
               Left = 241
               Bottom = 114
               Right = 392
            End
            DisplayFlags = 280
            TopColumn = 4
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_1'
GO
/****** Object:  ForeignKey [FK_br1-preporuceni dodaci za spoljasnju kruznu obradu brusenjem_Duzine pripremaka]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Dodaci za spoljasnju kruznu obradu brusenjem]  WITH CHECK ADD  CONSTRAINT [FK_br1-preporuceni dodaci za spoljasnju kruznu obradu brusenjem_Duzine pripremaka] FOREIGN KEY([DuzinaID])
REFERENCES [dbo].[Duzine obradaka] ([DuzinaID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Dodaci za spoljasnju kruznu obradu brusenjem] CHECK CONSTRAINT [FK_br1-preporuceni dodaci za spoljasnju kruznu obradu brusenjem_Duzine pripremaka]
GO
/****** Object:  ForeignKey [FK_br1-preporuceni dodaci za spoljasnju kruznu obradu brusenjem_Precnici pripremaka]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Dodaci za spoljasnju kruznu obradu brusenjem]  WITH CHECK ADD  CONSTRAINT [FK_br1-preporuceni dodaci za spoljasnju kruznu obradu brusenjem_Precnici pripremaka] FOREIGN KEY([PrecnikID])
REFERENCES [dbo].[Precnici obradaka] ([PrecinikID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Dodaci za spoljasnju kruznu obradu brusenjem] CHECK CONSTRAINT [FK_br1-preporuceni dodaci za spoljasnju kruznu obradu brusenjem_Precnici pripremaka]
GO
/****** Object:  ForeignKey [FK_Dodaci za uzduznu obradu struganjem_Duzine pripremaka]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Dodaci za uzduznu obradu struganjem]  WITH CHECK ADD  CONSTRAINT [FK_Dodaci za uzduznu obradu struganjem_Duzine pripremaka] FOREIGN KEY([DuzinaID])
REFERENCES [dbo].[Duzine obradaka] ([DuzinaID])
GO
ALTER TABLE [dbo].[Dodaci za uzduznu obradu struganjem] CHECK CONSTRAINT [FK_Dodaci za uzduznu obradu struganjem_Duzine pripremaka]
GO
/****** Object:  ForeignKey [FK_Dodaci za uzduznu obradu struganjem_Precnici pripremaka]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Dodaci za uzduznu obradu struganjem]  WITH CHECK ADD  CONSTRAINT [FK_Dodaci za uzduznu obradu struganjem_Precnici pripremaka] FOREIGN KEY([PrecnikID])
REFERENCES [dbo].[Precnici obradaka] ([PrecinikID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Dodaci za uzduznu obradu struganjem] CHECK CONSTRAINT [FK_Dodaci za uzduznu obradu struganjem_Precnici pripremaka]
GO
/****** Object:  ForeignKey [FK_Drzaci alata_DuzineAlata]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Drzaci alata]  WITH CHECK ADD  CONSTRAINT [FK_Drzaci alata_DuzineAlata] FOREIGN KEY([DuzinaAlata])
REFERENCES [dbo].[Duzine alata] ([DuzinaAlata])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Drzaci alata] CHECK CONSTRAINT [FK_Drzaci alata_DuzineAlata]
GO
/****** Object:  ForeignKey [FK_Drzaci alata_Ledjni uglovi plocica]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Drzaci alata]  WITH CHECK ADD  CONSTRAINT [FK_Drzaci alata_Ledjni uglovi plocica] FOREIGN KEY([LedjniUgao])
REFERENCES [dbo].[Ledjni uglovi plocica] ([LedjniUgao])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Drzaci alata] CHECK CONSTRAINT [FK_Drzaci alata_Ledjni uglovi plocica]
GO
/****** Object:  ForeignKey [FK_Drzaci alata_NaciniPricvrscavanjaPlocice]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Drzaci alata]  WITH CHECK ADD  CONSTRAINT [FK_Drzaci alata_NaciniPricvrscavanjaPlocice] FOREIGN KEY([PricvrscivanjePlocice])
REFERENCES [dbo].[Nacini pricvrscavanja plocice] ([PricvrscivanjaPlocice])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Drzaci alata] CHECK CONSTRAINT [FK_Drzaci alata_NaciniPricvrscavanjaPlocice]
GO
/****** Object:  ForeignKey [FK_Drzaci alata_Napadni uglovi]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Drzaci alata]  WITH CHECK ADD  CONSTRAINT [FK_Drzaci alata_Napadni uglovi] FOREIGN KEY([NapadniUgao])
REFERENCES [dbo].[Napadni uglovi] ([NapadniUgao])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Drzaci alata] CHECK CONSTRAINT [FK_Drzaci alata_Napadni uglovi]
GO
/****** Object:  ForeignKey [FK_Drzaci alata_Oblici plocica]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Drzaci alata]  WITH CHECK ADD  CONSTRAINT [FK_Drzaci alata_Oblici plocica] FOREIGN KEY([OblikPlocice])
REFERENCES [dbo].[Oblici plocica] ([OblikPlocice])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Drzaci alata] CHECK CONSTRAINT [FK_Drzaci alata_Oblici plocica]
GO
/****** Object:  ForeignKey [FK_Eksperimenti_Izlazni parametri]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Eksperimenti]  WITH CHECK ADD  CONSTRAINT [FK_Eksperimenti_Izlazni parametri] FOREIGN KEY([IzlazniParametar])
REFERENCES [dbo].[Izlazni parametri] ([IzlazniParametar])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Eksperimenti] CHECK CONSTRAINT [FK_Eksperimenti_Izlazni parametri]
GO
/****** Object:  ForeignKey [FK_Eksperimenti_Materijali]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Eksperimenti]  WITH CHECK ADD  CONSTRAINT [FK_Eksperimenti_Materijali] FOREIGN KEY([CMC])
REFERENCES [dbo].[Materijali] ([CMC])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Eksperimenti] CHECK CONSTRAINT [FK_Eksperimenti_Materijali]
GO
/****** Object:  ForeignKey [FK_Eksperimenti_Rezne plocice]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Eksperimenti]  WITH CHECK ADD  CONSTRAINT [FK_Eksperimenti_Rezne plocice] FOREIGN KEY([PlocicaID])
REFERENCES [dbo].[Rezne plocice] ([PlocicaID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Eksperimenti] CHECK CONSTRAINT [FK_Eksperimenti_Rezne plocice]
GO
/****** Object:  ForeignKey [FK_Eksperimenti_Ulazni parametri]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Eksperimenti]  WITH CHECK ADD  CONSTRAINT [FK_Eksperimenti_Ulazni parametri] FOREIGN KEY([UlazniParametar])
REFERENCES [dbo].[Ulazni parametri] ([UlazniParametar])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Eksperimenti] CHECK CONSTRAINT [FK_Eksperimenti_Ulazni parametri]
GO
/****** Object:  ForeignKey [FK_Izbor oblika plocice_Oblici plocica]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Izbor oblika plocice]  WITH CHECK ADD  CONSTRAINT [FK_Izbor oblika plocice_Oblici plocica] FOREIGN KEY([OblikPlocice])
REFERENCES [dbo].[Oblici plocica] ([OblikPlocice])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Izbor oblika plocice] CHECK CONSTRAINT [FK_Izbor oblika plocice_Oblici plocica]
GO
/****** Object:  ForeignKey [FK_Izbor oblika plocice_Obrade]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Izbor oblika plocice]  WITH CHECK ADD  CONSTRAINT [FK_Izbor oblika plocice_Obrade] FOREIGN KEY([ObradaID])
REFERENCES [dbo].[Obrade] ([ObradaID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Izbor oblika plocice] CHECK CONSTRAINT [FK_Izbor oblika plocice_Obrade]
GO
/****** Object:  ForeignKey [FK_Izmerene vrednosti eksperimenata_Eksperimenti]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Izmerene vrednosti eksperimenata]  WITH CHECK ADD  CONSTRAINT [FK_Izmerene vrednosti eksperimenata_Eksperimenti] FOREIGN KEY([NazivEksperimenta], [VremeIzvodjenjaEksperimenta])
REFERENCES [dbo].[Eksperimenti] ([NazivEksperimenta], [VremeIzvodjenjaEksperimenta])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Izmerene vrednosti eksperimenata] CHECK CONSTRAINT [FK_Izmerene vrednosti eksperimenata_Eksperimenti]
GO
/****** Object:  ForeignKey [FK_KlaseKvalitetaObradjenePovrsine_GradacijeObrada]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Klase kvaliteta obradjene povrsine]  WITH CHECK ADD  CONSTRAINT [FK_KlaseKvalitetaObradjenePovrsine_GradacijeObrada] FOREIGN KEY([GradacijaObrade])
REFERENCES [dbo].[Gradacije obrada] ([GradacijaObrade])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Klase kvaliteta obradjene povrsine] CHECK CONSTRAINT [FK_KlaseKvalitetaObradjenePovrsine_GradacijeObrada]
GO
/****** Object:  ForeignKey [FK_Kvaliteti obrade u zavisnosti od vrste materijala_Kvaliteti plocica]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Kvaliteti obrade u zavisnosti od vrste materijala]  WITH CHECK ADD  CONSTRAINT [FK_Kvaliteti obrade u zavisnosti od vrste materijala_Kvaliteti plocica] FOREIGN KEY([KvalitetObrade])
REFERENCES [dbo].[Kvaliteti plocica] ([KvalitetObrade])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Kvaliteti obrade u zavisnosti od vrste materijala] CHECK CONSTRAINT [FK_Kvaliteti obrade u zavisnosti od vrste materijala_Kvaliteti plocica]
GO
/****** Object:  ForeignKey [FK_Kvaliteti obrade u zavisnosti od vrste materijala_Vrste materijala]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Kvaliteti obrade u zavisnosti od vrste materijala]  WITH CHECK ADD  CONSTRAINT [FK_Kvaliteti obrade u zavisnosti od vrste materijala_Vrste materijala] FOREIGN KEY([VrstaMaterijala])
REFERENCES [dbo].[Vrste materijala] ([VrstaMaterijala])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Kvaliteti obrade u zavisnosti od vrste materijala] CHECK CONSTRAINT [FK_Kvaliteti obrade u zavisnosti od vrste materijala_Vrste materijala]
GO
/****** Object:  ForeignKey [FK_Kvaliteti plocica_Gradacije obrada]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Kvaliteti plocica]  WITH CHECK ADD  CONSTRAINT [FK_Kvaliteti plocica_Gradacije obrada] FOREIGN KEY([GradacijaObrade])
REFERENCES [dbo].[Gradacije obrada] ([GradacijaObrade])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Kvaliteti plocica] CHECK CONSTRAINT [FK_Kvaliteti plocica_Gradacije obrada]
GO
/****** Object:  ForeignKey [FK_Obrade_Tipovi obrada]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Obrade]  WITH CHECK ADD  CONSTRAINT [FK_Obrade_Tipovi obrada] FOREIGN KEY([TipObrade])
REFERENCES [dbo].[Tipovi obrada] ([TipObrade])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Obrade] CHECK CONSTRAINT [FK_Obrade_Tipovi obrada]
GO
/****** Object:  ForeignKey [FK_Obrade_Vrste obrada]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Obrade]  WITH CHECK ADD  CONSTRAINT [FK_Obrade_Vrste obrada] FOREIGN KEY([VrstaObrade])
REFERENCES [dbo].[Vrste obrada] ([VrstaObrade])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Obrade] CHECK CONSTRAINT [FK_Obrade_Vrste obrada]
GO
/****** Object:  ForeignKey [FK_Preporuceni rezimi rezanja_Kvaliteti Plocica]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Preporuceni rezimi rezanja]  WITH CHECK ADD  CONSTRAINT [FK_Preporuceni rezimi rezanja_Kvaliteti Plocica] FOREIGN KEY([Kvalitet])
REFERENCES [dbo].[Kvaliteti obrade] ([Kvalitet])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Preporuceni rezimi rezanja] CHECK CONSTRAINT [FK_Preporuceni rezimi rezanja_Kvaliteti Plocica]
GO
/****** Object:  ForeignKey [FK_Preporuceni rezimi rezanja_Rezne Plocice1]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Preporuceni rezimi rezanja]  WITH CHECK ADD  CONSTRAINT [FK_Preporuceni rezimi rezanja_Rezne Plocice1] FOREIGN KEY([PlocicaID])
REFERENCES [dbo].[Rezne plocice] ([PlocicaID])
GO
ALTER TABLE [dbo].[Preporuceni rezimi rezanja] CHECK CONSTRAINT [FK_Preporuceni rezimi rezanja_Rezne Plocice1]
GO
/****** Object:  ForeignKey [FK_Pripremci_Materijali]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Pripremci]  WITH CHECK ADD  CONSTRAINT [FK_Pripremci_Materijali] FOREIGN KEY([CMC])
REFERENCES [dbo].[Materijali] ([CMC])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pripremci] CHECK CONSTRAINT [FK_Pripremci_Materijali]
GO
/****** Object:  ForeignKey [FK_Pripremci_Precnici pripremaka]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Pripremci]  WITH CHECK ADD  CONSTRAINT [FK_Pripremci_Precnici pripremaka] FOREIGN KEY([Precnik])
REFERENCES [dbo].[Precnici pripremaka] ([Precnik])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pripremci] CHECK CONSTRAINT [FK_Pripremci_Precnici pripremaka]
GO
/****** Object:  ForeignKey [FK_Rezne Plocice_Kvaliteti obrade]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Rezne plocice]  WITH CHECK ADD  CONSTRAINT [FK_Rezne Plocice_Kvaliteti obrade] FOREIGN KEY([KvalitetObrade])
REFERENCES [dbo].[Kvaliteti plocica] ([KvalitetObrade])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rezne plocice] CHECK CONSTRAINT [FK_Rezne Plocice_Kvaliteti obrade]
GO
/****** Object:  ForeignKey [FK_Rezne Plocice_Ledjni uglovi plocica]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Rezne plocice]  WITH CHECK ADD  CONSTRAINT [FK_Rezne Plocice_Ledjni uglovi plocica] FOREIGN KEY([LedjniUgao])
REFERENCES [dbo].[Ledjni uglovi plocica] ([LedjniUgao])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rezne plocice] CHECK CONSTRAINT [FK_Rezne Plocice_Ledjni uglovi plocica]
GO
/****** Object:  ForeignKey [FK_Rezne Plocice_Oblici plocica]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Rezne plocice]  WITH CHECK ADD  CONSTRAINT [FK_Rezne Plocice_Oblici plocica] FOREIGN KEY([OblikPlocice])
REFERENCES [dbo].[Oblici plocica] ([OblikPlocice])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rezne plocice] CHECK CONSTRAINT [FK_Rezne Plocice_Oblici plocica]
GO
/****** Object:  ForeignKey [FK_Rezne Plocice_Tipovi plocica]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Rezne plocice]  WITH CHECK ADD  CONSTRAINT [FK_Rezne Plocice_Tipovi plocica] FOREIGN KEY([TipPlocice])
REFERENCES [dbo].[Tipovi plocica] ([TipPlocice])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rezne plocice] CHECK CONSTRAINT [FK_Rezne Plocice_Tipovi plocica]
GO
/****** Object:  ForeignKey [FK_Rezne Plocice_Tolerancije plocica]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Rezne plocice]  WITH CHECK ADD  CONSTRAINT [FK_Rezne Plocice_Tolerancije plocica] FOREIGN KEY([Tolerancija])
REFERENCES [dbo].[Tolerancije plocica] ([Tolerancija])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rezne plocice] CHECK CONSTRAINT [FK_Rezne Plocice_Tolerancije plocica]
GO
/****** Object:  ForeignKey [FK_Slike obrada_Obrade]    Script Date: 01/23/2017 05:52:42 ******/
ALTER TABLE [dbo].[Slike obrada]  WITH CHECK ADD  CONSTRAINT [FK_Slike obrada_Obrade] FOREIGN KEY([ObradaID])
REFERENCES [dbo].[Obrade] ([ObradaID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Slike obrada] CHECK CONSTRAINT [FK_Slike obrada_Obrade]
GO
