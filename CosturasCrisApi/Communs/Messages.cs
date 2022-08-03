namespace CosturasCrisApi.Communs
{
    public static class Messages
    {
        public const string ErrorMessage ="Error al cargar los registros. Contacte al administrador del sistema.";
        public const string ErrorAssociate = "No existe el usuario.";
        public const string ErrorNewCustomer = "Error al añadir el cliente. Consulte al administrador.";
        public const string ErrorUpdateCustomer = "Error al editar el cliente. Consulte al administrador.";
        public const string ErrorDeleteCustomer = "Error al eliminar. El cliente cuenta con uno o más pedidos.";
        public const string ErrorNewServiceProduct = "Error al añadir el servicio/producto. Consulte al administrador.";
        public const string ErrorUpdateServiceProduct = "Error al editar el servicio/producto. Consulte al administrador.";
        public const string ErrorDeleteServiceProduct = "Error al borrar. La compostura cuenta con uno o más pedidos.";
        public const string ErrorNewOrder = "Error al añadir nuevo pedido. Consulte al administrador.";
        public const string ErrorUpdateOrder = "Error al editar el pedido. Consulte al administrador.";
        public const string ErrorUpdateOrders = "Error al editar los pedidos. Consulte al administrador.";
        public const string ErrorDeleteOrder = "Error al borrar el pedido. Consulte al administrador.";
    }
}
