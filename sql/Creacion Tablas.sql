use almidb001;

create table inventario(
	id INT PRIMARY KEY IDENTITY(1,1),
	nombre varchar(50),
	descripcion varchar(50),
	cantidad int,
	tipo int
);

INSERT INTO inventario (nombre, descripcion, cantidad, tipo)
VALUES
('Procesador Intel i7', 'Pieza para computadora', 20, 1),
('Tarjeta madre ASUS', 'Pieza principal de la computadora', 15, 1),
('Memoria RAM DDR4 8GB', 'Pieza para mejorar el rendimiento', 30, 1),
('Tornillos para ensamblaje', 'Material para ensamblar equipos', 500, 2),
('Cables de conexi�n SATA', 'Material para conexiones internas', 100, 2),
('Pegamento conductivo', 'Material para reparaciones', 10, 2),
('Disco SSD 500GB', 'Pieza para almacenamiento r�pido', 12, 1),
('Kit de herramientas b�sicas', 'Material para reparaciones generales', 5, 2),
('Ventilador de refrigeraci�n', 'Pieza para evitar el sobrecalentamiento', 18, 1),
('Placas de circuito impreso (PCB)', 'Material para prototipos electr�nicos', 25, 2);



select * from inventario;

delete from inventario;

create table usuario(
	id INT PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(50),
	cedula VARCHAR(10),
	celular VARCHAR(10),
	correo VARCHAR(50),
	password VARCHAR(50)
);

-- Insertar 5 usuarios con datos ficticios
INSERT INTO usuario (nombre, cedula, celular, correo, password)
VALUES
('Usuario1', '1234567890', '5551234567', 'usuario1@email.com', 'contrase�a1'),
('Usuario2', '0987654321', '5559876543', 'usuario2@email.com', 'contrase�a2'),
('Usuario3', '1112233445', '5551112233', 'usuario3@email.com', 'contrase�a3'),
('Usuario4', '5556667778', '5555556666', 'usuario4@email.com', 'contrase�a4'),
('Usuario5', '9998887776', '5559998888', 'usuario5@email.com', 'contrase�a5');

select * from usuario;

create table orden(
	id INT PRIMARY KEY IDENTITY(1,1),
	tarea VARCHAR(50),
	fecha VARCHAR(50),
	estado VARCHAR(50),
	cliente VARCHAR(50),
	empleado_asignado VARCHAR(50)
);

-- Insertar 5 �rdenes con datos ficticios
INSERT INTO orden (tarea, fecha, estado, cliente, empleado_asignado)
VALUES
('Desarrollo de aplicaci�n m�vil', '2024-01-20', 'En progreso', 'Acme Corp', 'Juan P�rez'),
('Dise�o de p�gina web', '2024-01-21', 'Pendiente', 'ABC Ltd.', 'Ana G�mez'),
('Configuraci�n de servidores', '2024-01-22', 'Completada', 'XYZ Inc.', 'Carlos Rodr�guez'),
('Soporte t�cnico remoto', '2024-01-23', 'En espera', 'Tech Solutions', 'Mar�a L�pez'),
('Optimizaci�n de base de datos', '2024-01-24', 'Pendiente', 'Global Tech', 'Eduardo Garc�a');


select * from orden;
delete from orden;