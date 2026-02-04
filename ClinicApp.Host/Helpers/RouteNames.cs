namespace ClinicApp.Host.Helpers
{
    public static class RouteNames
    {
        // Patients
        public const string Patients_GetAll = "Patients_GetAll";
        public const string Patients_GetById = "Patients_GetById";
        public const string Patients_Create = "Patients_Create";
        public const string Patients_Update = "Patients_Update";
        public const string Patients_Delete = "Patients_Delete";

        // Users
        public const string Users_GetAll = "Users_GetAll";
        public const string Users_GetById = "Users_GetById";
        public const string Users_Create = "Users_Create";
        public const string Users_Update = "Users_Update";
        public const string Users_Delete = "Users_Delete";

        // Specialties (existing)
        public const string Specialties_GetAll = "Specialties_GetAll";
        public const string Specialties_GetById = "Specialties_GetById";
        public const string Specialties_Create = "Specialties_Create";
        public const string Specialties_Update = "Specialties_Update";
        public const string Specialties_Delete = "Specialties_Delete";

        // Add other controllers here (Appointments, Auth, etc.)
    }
}