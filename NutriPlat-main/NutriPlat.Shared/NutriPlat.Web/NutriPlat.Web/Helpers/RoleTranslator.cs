namespace NutriPlat.Web.Helpers
{
    public static class RoleTranslator
    {
        public static string ToSpanish(string? role)
        {
            return role switch
            {
                "Trainer" => "Entrenador",
                "Nutritionist" => "Nutriólogo",
                "Administrator" => "Administrador",
                "Admin" => "Administrador",
                "Usuario" => "Usuario",
                _ => "Sin rol"
            };
        }
    }
}
