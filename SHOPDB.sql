USE [ShopDB]
GO
/****** Object:  StoredProcedure [dbo].[_Safe_AllTransaction_During]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[_Safe_AllTransaction_During]
	@Day datetime,
	@Day2 datetime
AS
	SELECT * from TheSafe_Transactions where (SELECT CONVERT(date, Report_date))>=(SELECT CONVERT(date, @Day))
	AND (SELECT CONVERT(date, Report_date))<=(SELECT CONVERT(date, @Day2))
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[_Store_AllTransaction_DUring]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[_Store_AllTransaction_DUring]
	@Day datetime,
	@Day2 datetime
AS
	SELECT
	Bill_ID 'رقم الفاتورة',
	Bill_Type 'سبب الفاتورة',
	Report_Date 'تاريخ الفاتورة',
	replace(
	replace(Report_Type,1,'وارد')
	, 0,'صادر') 'نوع الفاتورة' 
	from StoreTransaction where (SELECT CONVERT(date, Report_date))>=(SELECT CONVERT(date, @Day))
	AND (SELECT CONVERT(date, Report_date))<=(SELECT CONVERT(date, @Day2))
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Backup_db]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Backup_db]
	@path nvarchar(200)
AS
	Backup database [ShopDB] to Disk =@path
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Band_selectAll]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Band_selectAll]
	
AS
	SELECT * from Outlay
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Customer_insertEXBillDetails]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Customer_insertEXBillDetails]
	@Bill_ID int ,
	@Material_ID int,
	@Material_Name nvarchar(150),
	@Material_PricePerUnit money,	
	@Material_Quantity int,
	@Unit nvarchar(50),
	@Total money,
	@Bill_Type bit,
	@Bill_Date datetime
AS
	INSERT into EXBillDetails VAlues (
	@Bill_ID,@Material_ID,@Material_Name,@Material_PricePerUnit,@Material_Quantity,@Unit,@Total,@Bill_Type,@Bill_Date
	)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Customer_insertPaybackBill]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Customer_insertPaybackBill]
	@Purchasing_ID int,
	@Customer_ID int ,
	@Bill_Date datetime,
	@Total_oldMoney money,
	@Payment_Money money,
	@After_Payment money,
	@Bill_Details nvarchar(200)
AS
	insert into EXPayback
	(Payback_ID,Customer_ID,Bill_Date,Total_oldMoney,Payment_Money,After_Payment)
	Values 
	(@Purchasing_ID,@Customer_ID,@Bill_Date,@Total_oldMoney,@Payment_Money,@After_Payment)

	insert into Customer_Transaction
	(Transaction_ID,Customer_ID,Report_Type,Report_date,Payment_Money,Material_Money,Transaction_Type)
	Values 
	(@Purchasing_ID,@Customer_ID,'فاتورة تسديد من العميل',@Bill_Date,@Payment_Money,0,0)

RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Customer_insertPurchasingBill]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Customer_insertPurchasingBill]
	@Purchasing_ID int,
	@Customer_ID int ,
	@Bill_Date datetime,
	@Material_Money money,
	@Discount_Money money,
	@After_Discount money,
	@Total_oldMoney money,
	@Total_Money money,
	@Payment_Money money,
	@After_Payment money
AS
	insert into EXPurchasing
	(PurchasingBill_ID,Customer_ID,Bill_Date,Material_Money,Discount_Money,After_Discount,Total_oldMoney,Total_Money,Payment_Money,After_Payment)
	Values 
	(@Purchasing_ID,@Customer_ID,@Bill_Date,@Material_Money,@Discount_Money,@After_Discount,@Total_oldMoney,@Total_Money,@Payment_Money,@After_Payment)

	insert into Customer_Transaction
	(Transaction_ID,Customer_ID,Report_Type,Report_date,Payment_Money,Material_Money,Transaction_Type)
	Values 
	(@Purchasing_ID,@Customer_ID,'فاتورة بيع الى عميل',@Bill_Date,@Payment_Money,@After_Discount,1)

RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Customer_insertReturningBill]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Customer_insertReturningBill]
	@Purchasing_ID int,
	@Customer_ID int ,
	@Bill_Date datetime,
	@Material_Money money,
	@Discount_Money money,
	@After_Discount money,
	@Total_oldMoney money,
	@Total_Money money
AS
	insert into EXReturning
	(ReturningBill_ID,Customer_ID,Bill_Date,Material_Money,Discount_Money,After_Discount,Total_oldMoney,Total_Money)
	Values 
	(@Purchasing_ID,@Customer_ID,@Bill_Date,@Material_Money,@Discount_Money,@After_Discount,@Total_oldMoney,@Total_Money)

	insert into Customer_Transaction
	(Transaction_ID,Customer_ID,Report_Type,Report_date,Payment_Money,Material_Money,Transaction_Type)
	Values 
	(@Purchasing_ID,@Customer_ID,'فاتورة مرتجع من عميل',@Bill_Date,@After_Discount,0,0)

RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Customer_selectAll]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Customer_selectAll]
AS
	SELECT 
	Customer_ID 'رقم المسلسل' ,
	Customer_Name 'اسم العميل' ,
	Customer_Phone 'رقم التليفون' ,
	Customer_Address 'عنوان العميل',
	Customer_Money 'جساب العميل'
	from Customer
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Customer_selectCustomerAccount]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Customer_selectCustomerAccount]
	@Day datetime,
	@Day2 datetime,
	@Customer_ID int
AS
	SELECT
	Transaction_ID 'رقم الفاتورة' ,
	Report_Type 'سبب الفاتورة' ,
	Report_date 'تاريخ الفاتورة' ,
	Payment_Money 'المبلغ الوارد' ,
	Material_Money 'المبلغ الصادر' ,
	replace(
	replace(Transaction_Type,0,'وارد')
	, 1,'صادر') 'نوع الفاتورة' 
	from Customer_Transaction where (SELECT CONVERT(date, Report_date))>=(SELECT CONVERT(date, @Day))
	AND (SELECT CONVERT(date, Report_date))<=(SELECT CONVERT(date, @Day2))
	AND Customer_ID=@Customer_ID
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Customer_selectCustomerAccount_Pay]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Customer_selectCustomerAccount_Pay]
	@Day datetime,
	@Day2 datetime,
	@Supplier_ID int
AS
	select isnull((SELECT SUM(Payment_Money) from Customer_Transaction where (SELECT CONVERT(date, Report_date))>=(SELECT CONVERT(date, @Day))
	AND (SELECT CONVERT(date, Report_date))<=(SELECT CONVERT(date, @Day2))
	AND Customer_ID=@Supplier_ID),0)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Customer_selectCustomerAccount_Purchasing]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Customer_selectCustomerAccount_Purchasing]
	@Day datetime,
	@Day2 datetime,
	@Supplier_ID int
AS
	select isnull((SELECT SUM(Material_Money) from Customer_Transaction where (SELECT CONVERT(date, Report_date))>=(SELECT CONVERT(date, @Day))
	AND (SELECT CONVERT(date, Report_date))<=(SELECT CONVERT(date, @Day2))
	AND Customer_ID=@Supplier_ID),0)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Customer_selectSearch]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Customer_selectSearch]
    @text nvarchar(150)
AS
	select * from Customer
	where Customer_Name like '%'+@text+'%'
								
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Customer_selectSearch_BYID]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Customer_selectSearch_BYID]
    @Customer_Id int
AS
	select * from Customer
	where Customer_ID=@Customer_Id
								
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Customer_updateCustomer]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[Customer_updateCustomer]
	@Customer_Name nvarchar(150),
    @Customer_Address nvarchar(150),
    @Customer_Phone nvarchar(50),
    @Customer_ID int
AS
	update Customer set Customer_Name=@Customer_Name,
	                    Customer_Address=@Customer_Address,
					 	Customer_Phone=@Customer_Phone
				where Customer_ID=@Customer_ID
								
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Customer_updateTotalMoney]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Customer_updateTotalMoney]
	@Customer_ID int ,
	@Total_Money money
AS
	update Customer set Customer_Money=@Total_Money
			where Customer_ID=@Customer_ID
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[EXPayback_selectID]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EXPayback_selectID]
AS
	Declare @i int = (ISNULL((select COUNT(Payback_ID) from EXPayback),0))
	select (@i+1)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[EXPurchasing_selectID]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EXPurchasing_selectID]
AS
	Declare @i int = (ISNULL((select COUNT(PurchasingBill_ID) from EXPurchasing),0))
	select (@i+1)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[EXReturning_selectID]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EXReturning_selectID]
AS
	Declare @i int = (ISNULL((select COUNT(ReturningBill_ID) from EXReturning),0))
	select (@i+1)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[IMPayback_selectID]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IMPayback_selectID]
AS
	Declare @i int = (ISNULL((select COUNT(Payback_ID) from IMPayback),0))
	select (@i+1)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[IMPurchasing_selectID]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IMPurchasing_selectID]
AS
	Declare @i int = (ISNULL((select COUNT(PurchasingBill_ID) from IMPurchasing),0))
	select (@i+1)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[IMReturning_selectID]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IMReturning_selectID]
AS
	Declare @i int = (ISNULL((select COUNT(ReturningBill_ID) from IMReturning),0))
	select (@i+1)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[insert_Customer]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[insert_Customer]
	@customerName nvarchar(150),
    @customerAddress nvarchar(150),
    @customerphone nvarchar(50),
    @customerMoney money
AS
	insert into Customer values (@customerName,@customerAddress,@customerphone,@customerMoney)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[insert_Outlay]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[insert_Outlay]
	@outlay_name nvarchar(150)
AS
	insert into Outlay (Outlay_Name) Values (@outlay_name)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[insert_Product]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[insert_Product]
	@Product_Name nvarchar(150),
	@Product_Price money,
	@Product_Sell money,
	@Product_Quantity int,
	@Product_Code nvarchar(150)
AS
	insert into Products Values (@Product_Name,@Product_Price,@Product_Sell,@Product_Quantity,@Product_Code)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[insert_Supplier]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[insert_Supplier]
	@Supplier_Name nvarchar(150),
    @Supplier_Address nvarchar(150),
    @Supplier_Phone nvarchar(50),
    @total money
AS
	insert into Supplier values (@Supplier_Name,@Supplier_Address,@Supplier_Phone,@total)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[insert_Users]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[insert_Users]
	@Username nvarchar(150),
	@Password nvarchar(50),
	@customerType bit
AS
	insert into Users Values (@Username,@Password,@customerType)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Outlay_During]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Outlay_During]
	@Day datetime,
	@Day2 datetime
AS
	SELECT
	Transaction_ID 'رقم الفاتورة',
	Outlay_Name 'بند الصرف',
	Report_Date 'تاريخ الفاتورة',
	Report_Total 'مبلغ الصرف',
	Report_Notes 'الملاحظات'
	from Outlay_Transactions , Outlay where (SELECT CONVERT(date, Report_date))>=(SELECT CONVERT(date, @Day))
	AND (SELECT CONVERT(date, Report_date))<=(SELECT CONVERT(date, @Day2))
	AND Outlay.Outlay_ID=Outlay_Transactions.Report_Band
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Outlay_During_Sum]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Outlay_During_Sum]
	@Day datetime,
	@Day2 datetime
AS
	SELECT
	isnull(SUM(Report_Total),0)
	from Outlay_Transactions where (SELECT CONVERT(date, Report_date))>=(SELECT CONVERT(date, @Day))
	AND (SELECT CONVERT(date, Report_date))<=(SELECT CONVERT(date, @Day2))
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Outlay_insertOutlayTransaction]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Outlay_insertOutlayTransaction]
	@Report_ID int ,
	@Report_Total money ,
	@Report_Notes nvarchar(250) ,
	@Report_Date datetime ,
	@Report_Band int 
AS
	insert into Outlay_Transactions VALUES (@Report_ID,@Report_Total,@Report_Notes,@Report_Date,@Report_Band)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Outlay_selectID]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Outlay_selectID]
AS
	Declare @i int = (ISNULL((select COUNT(Transaction_ID) from Outlay_Transactions),0))
	select (@i+1)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Outlay_sumBand]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Outlay_sumBand]
	@Day datetime,
	@Day2 datetime,
	@Band_iD int
AS
	SELECT
	isnull(SUM(Report_Total),0)
	from Outlay_Transactions where (SELECT CONVERT(date, Report_date))>=(SELECT CONVERT(date, @Day))
	AND (SELECT CONVERT(date, Report_date))<=(SELECT CONVERT(date, @Day2))
	AND Report_Band=@Band_iD
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Product_selectAll]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Product_selectAll]

AS
	SELECT 
	Product_id 'رقم المنتج',
	(Product_Code+' '+Product_Name) 'اسم المنتج',
	Product_Quantity 'الكمية المتاحة',
	Product_Price 'سعر الشراء',
	Product_Sell 'سعر البيع',
	(Product_Sell-Product_Price) 'الربح'
	
	from Products
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Product_selectGard]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Product_selectGard]
AS
	SELECT
	Product_id 'رقم المنتج',
	Product_Code 'كود المنتج',
	Product_Name 'اسم المنتج',
	Product_Quantity 'الكمية المتاحة',
	Product_Price 'سعر الشراء',
	Product_Sell 'سعر البيع',
	(Product_Sell-Product_Price) 'الربح',
	(Product_Quantity*(Product_Sell-Product_Price)) as 'اجمالى الربح'
	from Products
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Product_selectSearch]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Product_selectSearch]
	@text nvarchar(50)
AS
	select
		Product_id 'رقم المنتج',
	Product_Code 'كود المنتج',
	Product_Name 'اسم المنتج',
	Product_Quantity 'الكمية المتاحة',
	Product_Price 'سعر الشراء',
	Product_Sell 'سعر البيع',
	(Product_Sell-Product_Price) 'الربح'
	from Products
		where Product_Name like '%'+@text+'%' OR Product_Code like '%'+@text+'%'
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Product_selectSearch_BYID]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Product_selectSearch_BYID]
	@Product_ID int
AS
	select 
	Product_Name ,
	Product_id ,
	Product_Price ,
	Product_Sell ,
	Product_Quantity,
	Product_Code
	from Products
	where Product_id=@Product_ID
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Product_updateProduct]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Product_updateProduct]
	@Product_Name nvarchar(150),
	@Product_Price money,
	@Product_Sell money,
	@Product_ID int
AS
	update Products set 
			Product_Name=@Product_Name,
			Product_Price=@Product_Price,
			Product_Sell=@Product_Sell
		where Product_id=@Product_ID
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Product_updateQuantity_Decrease]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Product_updateQuantity_Decrease]
	@Material_ID int ,
	@Material_Quantity int
AS
	Declare @Quan int = (select isnull((SELECT Product_Quantity from Products where Product_id=@Material_ID),0))
	Update Products set Product_Quantity=(@Material_Quantity-@Quan) where Product_id=@Material_ID
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Product_updateQuantity_Increase]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Product_updateQuantity_Increase]
	@Material_ID int ,
	@Material_Quantity int
AS
	Declare @Quan int = (select isnull((SELECT Product_Quantity from Products where Product_id=@Material_ID),0))
	Update Products set Product_Quantity=(@Material_Quantity+@Quan) where Product_id=@Material_ID
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Report_Band]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Report_Band]
	@Report_ID int ,
	@Report_Total money ,
	@Report_Notes nvarchar(250) ,
	@Report_Date datetime ,
	@Report_Band int
AS
	insert into Outlay_Transactions Values (@Report_ID,@Report_Total,@Report_Notes,@Report_Date,@Report_Band)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Safe_insertTransaction]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Safe_insertTransaction]
	@Report_Type bit,
	@Report_Date datetime,
	@Bill_ID int,
	@Bill_Type nvarchar(150),
	@Report_Money money
AS
	insert into TheSafe_Transactions Values (@Report_Type,@Bill_ID,@Bill_Type,@Report_Date,@Report_Money)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Safe_updateDecrease]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Safe_updateDecrease]
	@Money_Quantity int 
AS
	Declare @Quan int = (SELECT COUNT(*) from TheSafe)
	if(@Quan=0)
	begin
		insert into TheSafe (Safe_Money) Values ((0-@Money_Quantity)) 
	end
	else
	begin
		Declare @old int = (select Safe_Money from TheSafe)
		UPDATE TheSafe set Safe_Money=(@old-@Money_Quantity)
	end
	
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Safe_updateIncrease]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Safe_updateIncrease]
	@Money_Quantity int 
AS
	Declare @Quan int = (SELECT COUNT(*) from TheSafe)
	if(@Quan=0)
	begin
		insert into TheSafe (Safe_Money) Values ((0+@Money_Quantity)) 
	end
	else
	begin
		Declare @old int = (select Safe_Money from TheSafe)
		UPDATE TheSafe set Safe_Money=(@old+@Money_Quantity)
	end
	
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Select_All]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Select_All]

AS
	SELECT  Product_id 'رقم المنتج' , Product_Name 'اسم المنتج' from Products
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[select_CountUsers]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[select_CountUsers]
	
AS
	SELECT COUNT(user_id) from Users
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[select_isValid]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[select_isValid]
	@Username nvarchar(150),
	@Password nvarchar(50),
	@isAdmin bit
AS
	SELECT * from Users where Username=@Username AND Users.Password=@Password AND isAdmin=@isAdmin
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[select_SafeMoney]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[select_SafeMoney]
AS
	SELECT Safe_Money from TheSafe
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[select_SUMofProduct__EX]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[select_SUMofProduct__EX]
	@Day datetime,
	@Day2 datetime,
	@Product_ID int
AS
	Declare @I int =(select isnull((SELECT SUM(Material_Quantity) from EXBillDetails
	where (SELECT CONVERT(date, Bill_Date))>=(SELECT CONVERT(date, @Day))
	AND (SELECT CONVERT(date, Bill_Date))<=(SELECT CONVERT(date, @Day2))
	AND Material_ID=@Product_ID
	AND Bill_Type=0),0))

	Declare @II int =(select isnull((SELECT SUM(Material_Quantity) from IMBillDetails
	where (SELECT CONVERT(date, Bill_Date))>=(SELECT CONVERT(date, @Day))
	AND (SELECT CONVERT(date, Bill_Date))<=(SELECT CONVERT(date, @Day2))
	AND Material_ID=@Product_ID
	AND Bill_Type=0),0))

	select @I+@II
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[select_SUMofProduct__IM]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[select_SUMofProduct__IM]
	@Day datetime,
	@Day2 datetime,
	@Product_ID int
AS
 	declare @I int =(select isnull((SELECT SUM(Material_Quantity) from IMBillDetails
	where (SELECT CONVERT(date, Bill_Date))>=(SELECT CONVERT(date, @Day))
	AND (SELECT CONVERT(date, Bill_Date))<=(SELECT CONVERT(date, @Day2))
	AND Material_ID=@Product_ID
	AND Bill_Type =1),0))

	declare @II int =(select isnull((SELECT SUM(Material_Quantity) from EXBillDetails
	where (SELECT CONVERT(date, Bill_Date))>=(SELECT CONVERT(date, @Day))
	AND (SELECT CONVERT(date, Bill_Date))<=(SELECT CONVERT(date, @Day2))
	AND Material_ID=@Product_ID
	AND Bill_Type =1),0))

	select @I+@II

RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[selectBillDetails]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[selectBillDetails]
	@BillID int
AS
	SELECT
	Material_ID 'رقم مسلسل',
	Material_Name 'اسم الصنف',
	Material_Quantity 'الكمية',
	Material_PricePerUnit 'السعر',
	Total 'الاجمالى',
	Product_Code 'كود المنتج'
	from EXBillDetails , Products
	Where Purchasing_Bill_ID=@BillID
	AND Products.Product_id=EXBillDetails.Material_ID
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[StoreTransaction_insert]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[StoreTransaction_insert]
	@Report_Type bit,
	@Report_Date datetime,
	@Bill_ID int,
	@Bill_Type nvarchar(150)
AS
	insert into StoreTransaction Values (@Report_Type,@Report_Date,@Bill_ID,@Bill_Type)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Supplier_insertIMBillDetails]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Supplier_insertIMBillDetails]
	@Bill_ID int ,
	@Material_ID int,
	@Material_Name nvarchar(150),
	@Material_PricePerUnit money,	
	@Material_Quantity int,
	@Unit nvarchar(50),
	@Total money,
	@Bill_Type bit,
	@Bill_Date datetime
AS
	INSERT into IMBillDetails VAlues (
	@Bill_ID,@Material_ID,@Material_Name,@Material_PricePerUnit,@Material_Quantity,@Unit,@Total,@Bill_Type,@Bill_Date
	)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Supplier_insertPaybackBill]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Supplier_insertPaybackBill]
	@Purchasing_ID int,
	@Supplier_ID int ,
	@Bill_Date datetime,
	@Total_oldMoney money,
	@Payment_Money money,
	@After_Payment money,
	@Bill_Details nvarchar(200),
	@Bill_Number_Supplier int
AS
	insert into IMPayback
	(Payback_ID,Supplier_ID,Bill_Date,Total_oldMoney,Payment_Money,After_Payment,Bill_Number_Supplier)
	Values 
	(@Purchasing_ID,@Supplier_ID,@Bill_Date,@Total_oldMoney,@Payment_Money,@After_Payment,@Bill_Number_Supplier)

	insert into Supplier_Transactions
	(Transaction_ID,Supplier_ID,Report_Type,Report_date,Payment_Money,Material_Money,Transaction_Type)
	Values 
	(@Purchasing_ID,@Supplier_ID,'فاتورة تسديد الى المورد',@Bill_Date,@Payment_Money,0,0)

RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Supplier_insertPurchasingBill]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Supplier_insertPurchasingBill]
	@Purchasing_ID int,
	@Supplier_ID int ,
	@Bill_Date datetime,
	@Material_Money money,
	@Discount_Money money,
	@After_Discount money,
	@Total_oldMoney money,
	@Total_Money money,
	@Payment_Money money,
	@After_Payment money,
	@Bill_Number_Supplier int
AS
	insert into IMPurchasing
	(PurchasingBill_ID,Supplier_ID,Bill_Date,Material_Money,Discount_Money,After_Discount,Total_oldMoney,Total_Money,Payment_Money,After_Payment,Bill_Number_Supplier)
	Values 
	(@Purchasing_ID,@Supplier_ID,@Bill_Date,@Material_Money,@Discount_Money,@After_Discount,@Total_oldMoney,@Total_Money,@Payment_Money,@After_Payment,@Bill_Number_Supplier)

	insert into Supplier_Transactions
	(Transaction_ID,Supplier_ID,Report_Type,Report_date,Payment_Money,Material_Money,Transaction_Type)
	Values 
	(@Purchasing_ID,@Supplier_ID,'فاتورة شراء من المورد',@Bill_Date,@Payment_Money,@After_Discount,1)

RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Supplier_insertReturningBill]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Supplier_insertReturningBill]
	@Purchasing_ID int,
	@Supplier_ID int ,
	@Bill_Date datetime,
	@Material_Money money,
	@Discount_Money money,
	@After_Discount money,
	@Total_oldMoney money,
	@Total_Money money
	
AS
	insert into IMReturning
	(ReturningBill_ID,Supplier_ID,Bill_Date,Material_Money,Discount_Money,After_Discount,Total_oldMoney,Total_Money)
	Values 
	(@Purchasing_ID,@Supplier_ID,@Bill_Date,@Material_Money,@Discount_Money,@After_Discount,@Total_oldMoney,@Total_Money)

	insert into Supplier_Transactions
	(Transaction_ID,Supplier_ID,Report_Type,Report_date,Payment_Money,Material_Money,Transaction_Type)
	Values 
	(@Purchasing_ID,@Supplier_ID,'فاتورة مرتجع الى المورد',@Bill_Date,@After_Discount,0,0)

RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Supplier_selectAll]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Supplier_selectAll]
AS
	SELECT
	Supplier_ID 'رقم المسلسل' ,
	Supplier_Name 'اسم المورد',
	Supplier_Phone 'هاتف المورد',
	Supplier_Address 'عنوان المورد',
	Supplier_Money 'حساب المورد'
	from Supplier
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Supplier_selectSearch]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Supplier_selectSearch]
    @text nvarchar(150)
AS
	select
	Supplier_ID 'رقم المسلسل' ,
	Supplier_Name 'اسم المورد',
	Supplier_Phone 'هاتف المورد',
	Supplier_Address 'عنوان المورد',
	Supplier_Money 'حساب المورد'
	from Supplier
	where Supplier_Name like '%'+@text+'%'
								
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Supplier_selectSearch_BYID]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Supplier_selectSearch_BYID]
    @Supplier_ID int
AS
	select 
	Supplier_ID ,
	Supplier_Name ,
	Supplier_Phone ,
	Supplier_Address ,
	Supplier_Money 
	from Supplier
	where Supplier_ID=@Supplier_ID
								
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Supplier_selectSupplierAccount]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Supplier_selectSupplierAccount]
	@Day datetime,
	@Day2 datetime,
	@Supplier_ID int
AS
	SELECT
	Transaction_ID 'رقم الفاتورة' ,
	Report_Type 'سبب الفاتورة' ,
	Report_date 'تاريخ الفاتورة' ,
	Payment_Money 'المبلغ المدفوع' ,
	Material_Money 'المبلغ الوارد' ,
	replace(
	replace(Transaction_Type,1,'وارد')
	, 0,'صادر') 'نوع الفاتورة' 
	from Supplier_Transactions where (SELECT CONVERT(date, Report_date))>=(SELECT CONVERT(date, @Day))
	AND (SELECT CONVERT(date, Report_date))<=(SELECT CONVERT(date, @Day2))
	AND Supplier_ID=@Supplier_ID
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Supplier_selectSupplierAccount_Pay]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Supplier_selectSupplierAccount_Pay]
	@Day datetime,
	@Day2 datetime,
	@Supplier_ID int
AS
	select isnull((SELECT SUM(Payment_Money) from Supplier_Transactions where (SELECT CONVERT(date, Report_date))>=(SELECT CONVERT(date, @Day))
	AND (SELECT CONVERT(date, Report_date))<=(SELECT CONVERT(date, @Day2))
	AND Supplier_ID=@Supplier_ID),0)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Supplier_selectSupplierAccount_Purchasing]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Supplier_selectSupplierAccount_Purchasing]
	@Day datetime,
	@Day2 datetime,
	@Supplier_ID int
AS
	select isnull((SELECT SUM(Material_Money) from Supplier_Transactions where (SELECT CONVERT(date, Report_date))>=(SELECT CONVERT(date, @Day))
	AND (SELECT CONVERT(date, Report_date))<=(SELECT CONVERT(date, @Day2))
	AND Supplier_ID=@Supplier_ID),0)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Supplier_updateSupplier]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Supplier_updateSupplier]
	@Supplier_Name nvarchar(150),
    @Supplier_Address nvarchar(150),
    @Supplier_Phone nvarchar(50),
    @Supplier_ID int
AS
	update Supplier set Supplier_Name=@Supplier_Name,
	                    Supplier_Address=@Supplier_Address,
					 	Supplier_Phone=@Supplier_Phone
				where Supplier_ID=@Supplier_ID
								
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Supplier_updateTotalMoney]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Supplier_updateTotalMoney]
	@Supplier_ID int ,
	@Total_Money money
AS
	update Supplier set Supplier_Money=@Total_Money
			where Supplier_ID=@Supplier_ID
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[User_updateUser]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_updateUser]
	@Username nvarchar(150),
	@Password nvarchar(50),
	@userType bit,
	@user_ID int
AS
	update Users set 
		Username=@Username,
		Users.Password=@Password,
		isAdmin=@userType 
		where
		user_id=@user_ID
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Users_selectAll]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Users_selectAll]
	
AS
	SELECT
	Users.user_id 'الرقم المسلسل',
	Users.Username 'اسم المستخدم',
	Users.Password'كلمة المرور'
	from Users
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Users_selectSearch]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Users_selectSearch]
	@text nvarchar(150)
AS
	select
	Users.user_id 'الرقم المسلسل',
	Users.Username 'اسم المستخدم',
	Users.Password'كلمة المرور'
	from Users
		where Username like '%'+@text+'%'
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[Users_selectSearch_BYID]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Users_selectSearch_BYID]
	@user_ID int
AS
	select
	Users.user_id ,
	Users.Username ,
	Users.Password,
	Users.isAdmin

	from Users
		where
		Users.user_id=@user_ID
RETURN 0
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Customer_ID] [int] IDENTITY(1,1) NOT NULL,
	[Customer_Name] [nvarchar](150) NULL,
	[Customer_Address] [nvarchar](150) NULL,
	[Customer_Phone] [nvarchar](50) NULL,
	[Customer_Money] [money] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Customer_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer_Transaction]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer_Transaction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Transaction_ID] [int] NULL,
	[Customer_ID] [int] NULL,
	[Report_Type] [nvarchar](150) NULL,
	[Report_date] [datetime] NULL,
	[Payment_Money] [money] NULL,
	[Material_Money] [money] NULL,
	[Transaction_Type] [bit] NULL,
 CONSTRAINT [PK_Customer_Transaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Emad]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emad](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](150) NULL,
	[Pass] [nvarchar](150) NULL,
 CONSTRAINT [PK_Emad] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EXBillDetails]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EXBillDetails](
	[Purchasing_Bill_ID] [int] NULL,
	[Material_ID] [int] NULL,
	[Material_Name] [nvarchar](150) NULL,
	[Material_PricePerUnit] [money] NULL,
	[Material_Quantity] [int] NULL,
	[Unit] [nvarchar](50) NULL,
	[Total] [money] NULL,
	[Bill_Type] [bit] NULL,
	[Bill_Date] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EXPayback]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EXPayback](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Payback_ID] [int] NULL,
	[Customer_ID] [int] NULL,
	[Bill_Date] [datetime] NULL,
	[Total_oldMoney] [money] NULL,
	[Payment_Money] [money] NULL,
	[After_Payment] [money] NULL,
	[Bill_Details] [nvarchar](250) NULL,
 CONSTRAINT [PK_EXPayback] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EXPurchasing]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EXPurchasing](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PurchasingBill_ID] [int] NULL,
	[Customer_ID] [int] NULL,
	[Bill_Date] [datetime] NULL,
	[Material_Money] [money] NULL,
	[Discount_Money] [money] NULL,
	[After_Discount] [money] NULL,
	[Total_oldMoney] [money] NULL,
	[Total_Money] [money] NULL,
	[Payment_Money] [money] NULL,
	[After_Payment] [money] NULL,
 CONSTRAINT [PK_EXPurchasing] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EXReturning]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EXReturning](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReturningBill_ID] [int] NULL,
	[Customer_ID] [int] NULL,
	[Bill_Date] [datetime] NULL,
	[Material_Money] [money] NULL,
	[Discount_Money] [money] NULL,
	[After_Discount] [money] NULL,
	[Total_oldMoney] [money] NULL,
	[Total_Money] [money] NULL,
 CONSTRAINT [PK_EXReturning] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IMBillDetails]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IMBillDetails](
	[Purchasing_Bill_ID] [int] NULL,
	[Material_ID] [int] NULL,
	[Material_Name] [nvarchar](150) NULL,
	[Material_PricePerUnit] [money] NULL,
	[Material_Quantity] [int] NULL,
	[Unit] [nvarchar](50) NULL,
	[Total] [money] NULL,
	[Bill_Type] [bit] NULL,
	[Bill_Date] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IMPayback]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IMPayback](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Payback_ID] [int] NULL,
	[Supplier_ID] [int] NULL,
	[Bill_Date] [datetime] NULL,
	[Total_oldMoney] [money] NULL,
	[Payment_Money] [money] NULL,
	[After_Payment] [money] NULL,
	[Bill_Details] [nvarchar](250) NULL,
	[Bill_Number_Supplier] [int] NULL,
 CONSTRAINT [PK_IMPayback] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IMPurchasing]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IMPurchasing](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PurchasingBill_ID] [int] NULL,
	[Supplier_ID] [int] NULL,
	[Bill_Date] [datetime] NULL,
	[Material_Money] [money] NULL,
	[Discount_Money] [money] NULL,
	[After_Discount] [money] NULL,
	[Total_oldMoney] [money] NULL,
	[Total_Money] [money] NULL,
	[Payment_Money] [money] NULL,
	[After_Payment] [money] NULL,
	[Bill_Number_Supplier] [int] NULL,
 CONSTRAINT [PK_IMPurchasing] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IMReturning]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IMReturning](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReturningBill_ID] [int] NULL,
	[Supplier_ID] [int] NULL,
	[Bill_Date] [datetime] NULL,
	[Material_Money] [money] NULL,
	[Discount_Money] [money] NULL,
	[After_Discount] [money] NULL,
	[Total_oldMoney] [money] NULL,
	[Total_Money] [money] NULL,
 CONSTRAINT [PK_IMReturning] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Outlay]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Outlay](
	[Outlay_ID] [int] IDENTITY(1,1) NOT NULL,
	[Outlay_Name] [nvarchar](150) NULL,
 CONSTRAINT [PK_Outlay] PRIMARY KEY CLUSTERED 
(
	[Outlay_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Outlay_Transactions]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Outlay_Transactions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Transaction_ID] [int] NULL,
	[Report_Total] [money] NULL,
	[Report_Notes] [nvarchar](250) NULL,
	[Report_Date] [datetime] NULL,
	[Report_Band] [int] NULL,
 CONSTRAINT [PK_Outlay_Transactions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Product_id] [int] IDENTITY(1,1) NOT NULL,
	[Product_Name] [nvarchar](150) NULL,
	[Product_Price] [money] NULL,
	[Product_Sell] [money] NULL,
	[Product_Quantity] [int] NULL,
	[Product_Code] [nvarchar](150) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreTransaction]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreTransaction](
	[Report_ID] [int] IDENTITY(1,1) NOT NULL,
	[Report_Type] [bit] NULL,
	[Report_Date] [datetime] NULL,
	[Bill_ID] [int] NULL,
	[Bill_Type] [nvarchar](150) NULL,
 CONSTRAINT [PK_StoreTransaction] PRIMARY KEY CLUSTERED 
(
	[Report_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[Supplier_ID] [int] IDENTITY(1,1) NOT NULL,
	[Supplier_Name] [nvarchar](150) NULL,
	[Supplier_Address] [nvarchar](150) NULL,
	[Supplier_Phone] [nvarchar](50) NULL,
	[Supplier_Money] [money] NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[Supplier_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Supplier_Transactions]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier_Transactions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Transaction_ID] [int] NULL,
	[Supplier_ID] [int] NULL,
	[Report_Type] [nvarchar](150) NULL,
	[Report_date] [datetime] NULL,
	[Payment_Money] [money] NULL,
	[Material_Money] [money] NULL,
	[Transaction_Type] [bit] NULL,
 CONSTRAINT [PK_Supplier_Transactions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TheSafe]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TheSafe](
	[Safe_ID] [int] IDENTITY(1,1) NOT NULL,
	[Safe_Money] [money] NULL,
 CONSTRAINT [PK_Safe] PRIMARY KEY CLUSTERED 
(
	[Safe_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TheSafe_Transactions]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TheSafe_Transactions](
	[Report_ID] [int] IDENTITY(1,1) NOT NULL,
	[Report_Type] [bit] NULL,
	[Bill_ID] [int] NULL,
	[Bill_Type] [nvarchar](150) NULL,
	[Report_Date] [datetime] NULL,
	[Report_Money] [money] NULL,
 CONSTRAINT [PK_TheSafe_Transactions] PRIMARY KEY CLUSTERED 
(
	[Report_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/2/2020 4:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](150) NULL,
	[Password] [nvarchar](50) NULL,
	[isAdmin] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
