Instalación y uso de Tailwind para este proyecto

1) Instalar dependencias (en la raíz del proyecto):
   npm install

2) Generar CSS de Tailwind una vez:
   npm run build:css

3) Para desarrollo con watch:
   npm run watch:css

4) Verificar que el archivo generado `wwwroot/css/site.tailwind.css` exista y esté referenciado en `Views/Shared/_Layout.cshtml`.

Notas:
- El `tailwind.config.js` está configurado para escanear archivos `*.cshtml` y `wwwroot/js`.
- Si deseas reemplazar Bootstrap, quita las referencias a `bootstrap.min.css` y `bootstrap.bundle.min.js` en `_Layout.cshtml`.
