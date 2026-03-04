
        markdown# 🏢 Sistema de Nómina
### ASP.NET Core MVC + Entity Framework Core + SQL Server

> Proyecto académico — Integración de Software | 3er Nivel | PAO 2025-2026 Ciclo II

---

## 👥 Equipo de Desarrollo

| Rol | Integrante | Responsabilidad |
|-----|-----------|-----------------|
| Líder Técnico + Frontend | TU NOMBRE | Arquitectura, vistas Razor, Tailwind CSS |
| Desarrollador Backend | Dayron | Auth, Empleados, Departamentos, Asignaciones |
| Desarrollador Backend | Erick | Títulos, Salarios, Auditoría, Reportes |
| QA / Analista de Datos | Dayana | Base de datos, pruebas, documentación |

---

## 📋 Descripción

Sistema web de gestión de nómina empresarial que permite administrar
empleados, departamentos, salarios, títulos y reportes. Desarrollado
con arquitectura MVC en ASP.NET Core 8, Entity Framework Core y
SQL Server.

---

## ✅ Funcionalidades

- 🔐 *Autenticación* con hash BCrypt y roles (Admin / RRHH)
- 👤 *Gestión de Empleados* — CRUD completo con borrado lógico
- 🏢 *Gestión de Departamentos* — CRUD con asignación de empleados
- 🔗 *Asignaciones* — Empleado a departamento con validación de solapamiento
- 👔 *Gerentes* — Asignación con validación de exclusividad por fecha
- 📋 *Títulos/Cargos* — Histórico por empleado sin solapamiento
- 💰 *Salarios* — Vigencias, salario activo y auditoría automática
- 📊 *Reportes* — Nómina vigente, cambios salariales, estructura org
- 📤 *Exportación* — PDF y Excel desde cualquier reporte
- 🔍 *Filtros y búsqueda* en todos los listados
- 📄 *Paginación* en todas las pantallas de consulta

---

## 🛠️ Tecnologías

| Tecnología | Versión | Uso |
|-----------|---------|-----|
| ASP.NET Core MVC | 8.0 | Framework principal |
| Entity Framework Core | 8.0 | ORM / acceso a datos |
| SQL Server | 2019+ | Base de datos |
| Tailwind CSS | CDN | Estilos y UI responsiva |
| BCrypt.Net | 4.0 | Hash de contraseñas |
| ClosedXML | - | Exportación Excel |
| iTextSharp | - | Exportación PDF |
    }
}
