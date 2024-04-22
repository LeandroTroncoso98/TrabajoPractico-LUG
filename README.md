# TrabajoPractico-LUG

    Enunciado del proyecto:
Una ferretería requiere un sistema para el control de sus productos, proveedores y clientes.
De los clientes se conoce código, nombre, apellido, DNI, mail, mientras que de los proveedores se conoce el 
código, razón social y CUIT.
La ferretería ofrece distintos tipos de productos, pero quiere comenzar con la parte de pinturería y electricidad.
Los productos poseen los siguientes datos en común el Código, descripción, marca, precio unitario, cantidad,
proveedor, y estado 
Además de las mismas características, los productos de pintura poseen color, y los eléctricos tienen una 
categoría que puede ser (cables, adaptadores y enchufes).
Un cliente puede comprar un producto de cualquier tipo.
Como la ferretería también está realizando descuentos, para poder acceder a los mismos el cliente compra un 
producto eléctrico recibe un descuento del 10%, y si compra productos de pinturería tiene un descuento del 
20%
Se pide
1. Desarrollar el modelo de clases que represente el problema.
2. Realizar el diagrama de clases en papel.
3. Utilizar jerarquía de herencia y polimorfismo. (obligatorio)
4. Implementar con XML (obligatorio)
5. Realizar el ABM de Productos y clientes, pueden hardcodear los proveedores (solo leer y traerlos del 
XML), y realizar la contratación del producto. (obligatorio)
6. La interfaz gráfica debe contened un MDI, y los formularios ABM correspondientes, como también 
datagridview y combos.
7. Informar:
a. El producto más contratado, por tipo y el monto.
b. El producto menos contratado, por tipo y monto
c. El monto total recaudado por tipo de producto
8. Utilizar expresiones regulares en el formulario de clientes, validando todos sus campos.
9. El/los repositorios de datos van a ser en XML, cree, y muestre los datos del/los XML. 
(obligatorio)
 Aclaraciones: 
1) La cantidad de capas mínima será UI-BE-BLL -MPP
2) El/los XML lo crean udes 
3) Al usar XML tengan en cuenta q es texto plano, convertir al tipo de dato de objeto. 
4) Los informes se realizan con el chart. 
5) Los XML deben tener elementos y atributos. 
6) La herencia y polimorfismo lo tienen que aplicar en la capa que corresponde ( es obligatorio)
7) Cada cliente puede contratar 1 solo producto ( relación 1 a 1 ).
8) No se puede asociar un producto que no tenga stock.

       Visual del proyecto
![image](https://github.com/LeandroTroncoso98/TrabajoPractico-LUG/assets/105368488/f0119f57-5087-436d-9e8d-33e4e5c31f38)

       UML:
![UML](https://github.com/LeandroTroncoso98/TrabajoPractico-LUG/assets/105368488/3e2a1920-36a9-4494-96c8-d1bbd388119c)

