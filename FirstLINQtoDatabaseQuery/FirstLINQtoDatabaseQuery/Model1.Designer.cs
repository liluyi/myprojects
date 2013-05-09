﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM 关系源元数据

[assembly: EdmRelationshipAttribute("NORTHWNDModel", "FK_Orders_Customers", "Customers", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(FirstLINQtoDatabaseQuery.Customers), "Orders", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(FirstLINQtoDatabaseQuery.Orders), true)]
[assembly: EdmRelationshipAttribute("NORTHWNDModel", "FK_Order_Details_Orders", "Orders", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(FirstLINQtoDatabaseQuery.Orders), "Order_Details", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(FirstLINQtoDatabaseQuery.Order_Details), true)]

#endregion

namespace FirstLINQtoDatabaseQuery
{
    #region 上下文
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    public partial class NORTHWNDEntities : ObjectContext
    {
        #region 构造函数
    
        /// <summary>
        /// 请使用应用程序配置文件的“NORTHWNDEntities”部分中的连接字符串初始化新 NORTHWNDEntities 对象。
        /// </summary>
        public NORTHWNDEntities() : base("name=NORTHWNDEntities", "NORTHWNDEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// 初始化新的 NORTHWNDEntities 对象。
        /// </summary>
        public NORTHWNDEntities(string connectionString) : base(connectionString, "NORTHWNDEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// 初始化新的 NORTHWNDEntities 对象。
        /// </summary>
        public NORTHWNDEntities(EntityConnection connection) : base(connection, "NORTHWNDEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region 分部方法
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet 属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        public ObjectSet<Customers> Customers
        {
            get
            {
                if ((_Customers == null))
                {
                    _Customers = base.CreateObjectSet<Customers>("Customers");
                }
                return _Customers;
            }
        }
        private ObjectSet<Customers> _Customers;
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        public ObjectSet<Order_Details> Order_Details
        {
            get
            {
                if ((_Order_Details == null))
                {
                    _Order_Details = base.CreateObjectSet<Order_Details>("Order_Details");
                }
                return _Order_Details;
            }
        }
        private ObjectSet<Order_Details> _Order_Details;
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        public ObjectSet<Orders> Orders
        {
            get
            {
                if ((_Orders == null))
                {
                    _Orders = base.CreateObjectSet<Orders>("Orders");
                }
                return _Orders;
            }
        }
        private ObjectSet<Orders> _Orders;

        #endregion
        #region AddTo 方法
    
        /// <summary>
        /// 用于向 Customers EntitySet 添加新对象的方法，已弃用。请考虑改用关联的 ObjectSet&lt;T&gt; 属性的 .Add 方法。
        /// </summary>
        public void AddToCustomers(Customers customers)
        {
            base.AddObject("Customers", customers);
        }
    
        /// <summary>
        /// 用于向 Order_Details EntitySet 添加新对象的方法，已弃用。请考虑改用关联的 ObjectSet&lt;T&gt; 属性的 .Add 方法。
        /// </summary>
        public void AddToOrder_Details(Order_Details order_Details)
        {
            base.AddObject("Order_Details", order_Details);
        }
    
        /// <summary>
        /// 用于向 Orders EntitySet 添加新对象的方法，已弃用。请考虑改用关联的 ObjectSet&lt;T&gt; 属性的 .Add 方法。
        /// </summary>
        public void AddToOrders(Orders orders)
        {
            base.AddObject("Orders", orders);
        }

        #endregion
    }
    

    #endregion
    
    #region 实体
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="NORTHWNDModel", Name="Customers")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Customers : EntityObject
    {
        #region 工厂方法
    
        /// <summary>
        /// 创建新的 Customers 对象。
        /// </summary>
        /// <param name="customerID">CustomerID 属性的初始值。</param>
        /// <param name="companyName">CompanyName 属性的初始值。</param>
        public static Customers CreateCustomers(global::System.String customerID, global::System.String companyName)
        {
            Customers customers = new Customers();
            customers.CustomerID = customerID;
            customers.CompanyName = companyName;
            return customers;
        }

        #endregion
        #region 基元属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String CustomerID
        {
            get
            {
                return _CustomerID;
            }
            set
            {
                if (_CustomerID != value)
                {
                    OnCustomerIDChanging(value);
                    ReportPropertyChanging("CustomerID");
                    _CustomerID = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("CustomerID");
                    OnCustomerIDChanged();
                }
            }
        }
        private global::System.String _CustomerID;
        partial void OnCustomerIDChanging(global::System.String value);
        partial void OnCustomerIDChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String CompanyName
        {
            get
            {
                return _CompanyName;
            }
            set
            {
                OnCompanyNameChanging(value);
                ReportPropertyChanging("CompanyName");
                _CompanyName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("CompanyName");
                OnCompanyNameChanged();
            }
        }
        private global::System.String _CompanyName;
        partial void OnCompanyNameChanging(global::System.String value);
        partial void OnCompanyNameChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String ContactName
        {
            get
            {
                return _ContactName;
            }
            set
            {
                OnContactNameChanging(value);
                ReportPropertyChanging("ContactName");
                _ContactName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("ContactName");
                OnContactNameChanged();
            }
        }
        private global::System.String _ContactName;
        partial void OnContactNameChanging(global::System.String value);
        partial void OnContactNameChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String ContactTitle
        {
            get
            {
                return _ContactTitle;
            }
            set
            {
                OnContactTitleChanging(value);
                ReportPropertyChanging("ContactTitle");
                _ContactTitle = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("ContactTitle");
                OnContactTitleChanged();
            }
        }
        private global::System.String _ContactTitle;
        partial void OnContactTitleChanging(global::System.String value);
        partial void OnContactTitleChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Address
        {
            get
            {
                return _Address;
            }
            set
            {
                OnAddressChanging(value);
                ReportPropertyChanging("Address");
                _Address = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Address");
                OnAddressChanged();
            }
        }
        private global::System.String _Address;
        partial void OnAddressChanging(global::System.String value);
        partial void OnAddressChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String City
        {
            get
            {
                return _City;
            }
            set
            {
                OnCityChanging(value);
                ReportPropertyChanging("City");
                _City = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("City");
                OnCityChanged();
            }
        }
        private global::System.String _City;
        partial void OnCityChanging(global::System.String value);
        partial void OnCityChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Region
        {
            get
            {
                return _Region;
            }
            set
            {
                OnRegionChanging(value);
                ReportPropertyChanging("Region");
                _Region = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Region");
                OnRegionChanged();
            }
        }
        private global::System.String _Region;
        partial void OnRegionChanging(global::System.String value);
        partial void OnRegionChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String PostalCode
        {
            get
            {
                return _PostalCode;
            }
            set
            {
                OnPostalCodeChanging(value);
                ReportPropertyChanging("PostalCode");
                _PostalCode = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("PostalCode");
                OnPostalCodeChanged();
            }
        }
        private global::System.String _PostalCode;
        partial void OnPostalCodeChanging(global::System.String value);
        partial void OnPostalCodeChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Country
        {
            get
            {
                return _Country;
            }
            set
            {
                OnCountryChanging(value);
                ReportPropertyChanging("Country");
                _Country = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Country");
                OnCountryChanged();
            }
        }
        private global::System.String _Country;
        partial void OnCountryChanging(global::System.String value);
        partial void OnCountryChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                OnPhoneChanging(value);
                ReportPropertyChanging("Phone");
                _Phone = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Phone");
                OnPhoneChanged();
            }
        }
        private global::System.String _Phone;
        partial void OnPhoneChanging(global::System.String value);
        partial void OnPhoneChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Fax
        {
            get
            {
                return _Fax;
            }
            set
            {
                OnFaxChanging(value);
                ReportPropertyChanging("Fax");
                _Fax = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Fax");
                OnFaxChanged();
            }
        }
        private global::System.String _Fax;
        partial void OnFaxChanging(global::System.String value);
        partial void OnFaxChanged();

        #endregion
    
        #region 导航属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("NORTHWNDModel", "FK_Orders_Customers", "Orders")]
        public EntityCollection<Orders> Orders
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Orders>("NORTHWNDModel.FK_Orders_Customers", "Orders");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Orders>("NORTHWNDModel.FK_Orders_Customers", "Orders", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="NORTHWNDModel", Name="Order_Details")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Order_Details : EntityObject
    {
        #region 工厂方法
    
        /// <summary>
        /// 创建新的 Order_Details 对象。
        /// </summary>
        /// <param name="orderID">OrderID 属性的初始值。</param>
        /// <param name="productID">ProductID 属性的初始值。</param>
        /// <param name="unitPrice">UnitPrice 属性的初始值。</param>
        /// <param name="quantity">Quantity 属性的初始值。</param>
        /// <param name="discount">Discount 属性的初始值。</param>
        public static Order_Details CreateOrder_Details(global::System.Int32 orderID, global::System.Int32 productID, global::System.Decimal unitPrice, global::System.Int16 quantity, global::System.Single discount)
        {
            Order_Details order_Details = new Order_Details();
            order_Details.OrderID = orderID;
            order_Details.ProductID = productID;
            order_Details.UnitPrice = unitPrice;
            order_Details.Quantity = quantity;
            order_Details.Discount = discount;
            return order_Details;
        }

        #endregion
        #region 基元属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 OrderID
        {
            get
            {
                return _OrderID;
            }
            set
            {
                if (_OrderID != value)
                {
                    OnOrderIDChanging(value);
                    ReportPropertyChanging("OrderID");
                    _OrderID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("OrderID");
                    OnOrderIDChanged();
                }
            }
        }
        private global::System.Int32 _OrderID;
        partial void OnOrderIDChanging(global::System.Int32 value);
        partial void OnOrderIDChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ProductID
        {
            get
            {
                return _ProductID;
            }
            set
            {
                if (_ProductID != value)
                {
                    OnProductIDChanging(value);
                    ReportPropertyChanging("ProductID");
                    _ProductID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ProductID");
                    OnProductIDChanged();
                }
            }
        }
        private global::System.Int32 _ProductID;
        partial void OnProductIDChanging(global::System.Int32 value);
        partial void OnProductIDChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal UnitPrice
        {
            get
            {
                return _UnitPrice;
            }
            set
            {
                OnUnitPriceChanging(value);
                ReportPropertyChanging("UnitPrice");
                _UnitPrice = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("UnitPrice");
                OnUnitPriceChanged();
            }
        }
        private global::System.Decimal _UnitPrice;
        partial void OnUnitPriceChanging(global::System.Decimal value);
        partial void OnUnitPriceChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int16 Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                OnQuantityChanging(value);
                ReportPropertyChanging("Quantity");
                _Quantity = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Quantity");
                OnQuantityChanged();
            }
        }
        private global::System.Int16 _Quantity;
        partial void OnQuantityChanging(global::System.Int16 value);
        partial void OnQuantityChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Single Discount
        {
            get
            {
                return _Discount;
            }
            set
            {
                OnDiscountChanging(value);
                ReportPropertyChanging("Discount");
                _Discount = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Discount");
                OnDiscountChanged();
            }
        }
        private global::System.Single _Discount;
        partial void OnDiscountChanging(global::System.Single value);
        partial void OnDiscountChanged();

        #endregion
    
        #region 导航属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("NORTHWNDModel", "FK_Order_Details_Orders", "Orders")]
        public Orders Orders
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Orders>("NORTHWNDModel.FK_Order_Details_Orders", "Orders").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Orders>("NORTHWNDModel.FK_Order_Details_Orders", "Orders").Value = value;
            }
        }
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Orders> OrdersReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Orders>("NORTHWNDModel.FK_Order_Details_Orders", "Orders");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Orders>("NORTHWNDModel.FK_Order_Details_Orders", "Orders", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="NORTHWNDModel", Name="Orders")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Orders : EntityObject
    {
        #region 工厂方法
    
        /// <summary>
        /// 创建新的 Orders 对象。
        /// </summary>
        /// <param name="orderID">OrderID 属性的初始值。</param>
        public static Orders CreateOrders(global::System.Int32 orderID)
        {
            Orders orders = new Orders();
            orders.OrderID = orderID;
            return orders;
        }

        #endregion
        #region 基元属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 OrderID
        {
            get
            {
                return _OrderID;
            }
            set
            {
                if (_OrderID != value)
                {
                    OnOrderIDChanging(value);
                    ReportPropertyChanging("OrderID");
                    _OrderID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("OrderID");
                    OnOrderIDChanged();
                }
            }
        }
        private global::System.Int32 _OrderID;
        partial void OnOrderIDChanging(global::System.Int32 value);
        partial void OnOrderIDChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String CustomerID
        {
            get
            {
                return _CustomerID;
            }
            set
            {
                OnCustomerIDChanging(value);
                ReportPropertyChanging("CustomerID");
                _CustomerID = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("CustomerID");
                OnCustomerIDChanged();
            }
        }
        private global::System.String _CustomerID;
        partial void OnCustomerIDChanging(global::System.String value);
        partial void OnCustomerIDChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> EmployeeID
        {
            get
            {
                return _EmployeeID;
            }
            set
            {
                OnEmployeeIDChanging(value);
                ReportPropertyChanging("EmployeeID");
                _EmployeeID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("EmployeeID");
                OnEmployeeIDChanged();
            }
        }
        private Nullable<global::System.Int32> _EmployeeID;
        partial void OnEmployeeIDChanging(Nullable<global::System.Int32> value);
        partial void OnEmployeeIDChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> OrderDate
        {
            get
            {
                return _OrderDate;
            }
            set
            {
                OnOrderDateChanging(value);
                ReportPropertyChanging("OrderDate");
                _OrderDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("OrderDate");
                OnOrderDateChanged();
            }
        }
        private Nullable<global::System.DateTime> _OrderDate;
        partial void OnOrderDateChanging(Nullable<global::System.DateTime> value);
        partial void OnOrderDateChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> RequiredDate
        {
            get
            {
                return _RequiredDate;
            }
            set
            {
                OnRequiredDateChanging(value);
                ReportPropertyChanging("RequiredDate");
                _RequiredDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("RequiredDate");
                OnRequiredDateChanged();
            }
        }
        private Nullable<global::System.DateTime> _RequiredDate;
        partial void OnRequiredDateChanging(Nullable<global::System.DateTime> value);
        partial void OnRequiredDateChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> ShippedDate
        {
            get
            {
                return _ShippedDate;
            }
            set
            {
                OnShippedDateChanging(value);
                ReportPropertyChanging("ShippedDate");
                _ShippedDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ShippedDate");
                OnShippedDateChanged();
            }
        }
        private Nullable<global::System.DateTime> _ShippedDate;
        partial void OnShippedDateChanging(Nullable<global::System.DateTime> value);
        partial void OnShippedDateChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ShipVia
        {
            get
            {
                return _ShipVia;
            }
            set
            {
                OnShipViaChanging(value);
                ReportPropertyChanging("ShipVia");
                _ShipVia = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ShipVia");
                OnShipViaChanged();
            }
        }
        private Nullable<global::System.Int32> _ShipVia;
        partial void OnShipViaChanging(Nullable<global::System.Int32> value);
        partial void OnShipViaChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> Freight
        {
            get
            {
                return _Freight;
            }
            set
            {
                OnFreightChanging(value);
                ReportPropertyChanging("Freight");
                _Freight = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Freight");
                OnFreightChanged();
            }
        }
        private Nullable<global::System.Decimal> _Freight;
        partial void OnFreightChanging(Nullable<global::System.Decimal> value);
        partial void OnFreightChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String ShipName
        {
            get
            {
                return _ShipName;
            }
            set
            {
                OnShipNameChanging(value);
                ReportPropertyChanging("ShipName");
                _ShipName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("ShipName");
                OnShipNameChanged();
            }
        }
        private global::System.String _ShipName;
        partial void OnShipNameChanging(global::System.String value);
        partial void OnShipNameChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String ShipAddress
        {
            get
            {
                return _ShipAddress;
            }
            set
            {
                OnShipAddressChanging(value);
                ReportPropertyChanging("ShipAddress");
                _ShipAddress = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("ShipAddress");
                OnShipAddressChanged();
            }
        }
        private global::System.String _ShipAddress;
        partial void OnShipAddressChanging(global::System.String value);
        partial void OnShipAddressChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String ShipCity
        {
            get
            {
                return _ShipCity;
            }
            set
            {
                OnShipCityChanging(value);
                ReportPropertyChanging("ShipCity");
                _ShipCity = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("ShipCity");
                OnShipCityChanged();
            }
        }
        private global::System.String _ShipCity;
        partial void OnShipCityChanging(global::System.String value);
        partial void OnShipCityChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String ShipRegion
        {
            get
            {
                return _ShipRegion;
            }
            set
            {
                OnShipRegionChanging(value);
                ReportPropertyChanging("ShipRegion");
                _ShipRegion = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("ShipRegion");
                OnShipRegionChanged();
            }
        }
        private global::System.String _ShipRegion;
        partial void OnShipRegionChanging(global::System.String value);
        partial void OnShipRegionChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String ShipPostalCode
        {
            get
            {
                return _ShipPostalCode;
            }
            set
            {
                OnShipPostalCodeChanging(value);
                ReportPropertyChanging("ShipPostalCode");
                _ShipPostalCode = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("ShipPostalCode");
                OnShipPostalCodeChanged();
            }
        }
        private global::System.String _ShipPostalCode;
        partial void OnShipPostalCodeChanging(global::System.String value);
        partial void OnShipPostalCodeChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String ShipCountry
        {
            get
            {
                return _ShipCountry;
            }
            set
            {
                OnShipCountryChanging(value);
                ReportPropertyChanging("ShipCountry");
                _ShipCountry = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("ShipCountry");
                OnShipCountryChanged();
            }
        }
        private global::System.String _ShipCountry;
        partial void OnShipCountryChanging(global::System.String value);
        partial void OnShipCountryChanged();

        #endregion
    
        #region 导航属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("NORTHWNDModel", "FK_Orders_Customers", "Customers")]
        public Customers Customers
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Customers>("NORTHWNDModel.FK_Orders_Customers", "Customers").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Customers>("NORTHWNDModel.FK_Orders_Customers", "Customers").Value = value;
            }
        }
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Customers> CustomersReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Customers>("NORTHWNDModel.FK_Orders_Customers", "Customers");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Customers>("NORTHWNDModel.FK_Orders_Customers", "Customers", value);
                }
            }
        }
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("NORTHWNDModel", "FK_Order_Details_Orders", "Order_Details")]
        public EntityCollection<Order_Details> Order_Details
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Order_Details>("NORTHWNDModel.FK_Order_Details_Orders", "Order_Details");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Order_Details>("NORTHWNDModel.FK_Order_Details_Orders", "Order_Details", value);
                }
            }
        }

        #endregion
    }

    #endregion
    
}