use almidb001;

create table inventario(
	id INT PRIMARY KEY IDENTITY(1,1),
	nombre varchar(50),
	descripcion varchar(50),
	cantidad int
);

INSERT INTO inventario (nombre, descripcion, cantidad)
VALUES
    ('Herramienta de corte', 'Sierra circular para madera', 5),
    ('Material de soldadura', 'Electrodos para soldadura de arco', 100),
    ('Tornillos', 'Tornillos de acero inoxidable, tamaño 1/4"', 500),
    ('Pintura', 'Lata de pintura acrílica, color negro', 10),
    ('Tableros de madera', 'Tableros contrachapados de 3/4"', 15),
    ('Llaves ajustables', 'Juego de llaves ajustables de diferentes tamaños', 8),
    ('Brocas para metal', 'Juego de brocas HSS para metal, tamaño 1-10mm', 20),
    ('Cinta métrica', 'Cinta métrica de 5 metros', 12),
    ('Guantes de trabajo', 'Guantes de trabajo resistentes', 50),
    ('Pegamento industrial', 'Adhesivo instantáneo para materiales diversos', 30);

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
('Usuario1', '1234567890', '5551234567', 'usuario1@email.com', 'contraseña1'),
('Usuario2', '0987654321', '5559876543', 'usuario2@email.com', 'contraseña2'),
('Usuario3', '1112233445', '5551112233', 'usuario3@email.com', 'contraseña3'),
('Usuario4', '5556667778', '5555556666', 'usuario4@email.com', 'contraseña4'),
('Usuario5', '9998887776', '5559998888', 'usuario5@email.com', 'contraseña5');

select * from usuario;

create table orden(
	id INT PRIMARY KEY IDENTITY(1,1),
	tarea VARCHAR(50),
	fecha VARCHAR(50),
	estado VARCHAR(50),
	cliente VARCHAR(50),
	empleado_asignado VARCHAR(50)
);

-- Insertar 5 órdenes con datos ficticios
INSERT INTO orden (tarea, fecha, estado, cliente, empleado_asignado)
VALUES
('Instalación de software', '2024-01-20', 'En progreso', 'Cliente1', 'Empleado1'),
('Mantenimiento de hardware', '2024-01-21', 'Pendiente', 'Cliente2', 'Empleado2'),
('Actualización de sistema', '2024-01-22', 'Completada', 'Cliente3', 'Empleado3'),
('Reparación de impresora', '2024-01-23', 'En espera', 'Cliente4', 'Empleado4'),
('Configuración de red', '2024-01-24', 'Pendiente', 'Cliente5', 'Empleado5');

select * from orden;