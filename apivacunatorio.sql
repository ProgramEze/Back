-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 02-09-2024 a las 12:34:22
-- Versión del servidor: 10.4.28-MariaDB
-- Versión de PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `apivacunatorio`
--
CREATE DATABASE IF NOT EXISTS `apivacunatorio` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `apivacunatorio`;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `agente`
--

CREATE TABLE `agente` (
  `Matricula` int(11) NOT NULL,
  `Clave` varchar(255) NOT NULL,
  `Nombre` varchar(50) NOT NULL,
  `Apellido` varchar(50) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `agente`
--

INSERT INTO `agente` (`Matricula`, `Clave`, `Nombre`, `Apellido`, `Email`, `Estado`) VALUES
(23948512, '3A0G2+zJ3luLnlC44+Xe5HGw/9RWJNoyF2XZACvev20=', 'Juan', 'Pérez', 'juan.perez@example.com', 1),
(24367895, '3A0G2+zJ3luLnlC44+Xe5HGw/9RWJNoyF2XZACvev20=', 'Ana', 'Fernández', 'ana.fernandez@example.com', 0),
(25678934, '3A0G2+zJ3luLnlC44+Xe5HGw/9RWJNoyF2XZACvev20=', 'Miguel', 'Sánchez', 'miguel.sanchez@example.com', 0),
(28765432, '3A0G2+zJ3luLnlC44+Xe5HGw/9RWJNoyF2XZACvev20=', 'Carlos', 'López', 'carlos.lopez@example.com', 1),
(31098765, '3A0G2+zJ3luLnlC44+Xe5HGw/9RWJNoyF2XZACvev20=', 'Sofía', 'Rodríguez', 'sofia.rodriguez@example.com', 1),
(34229421, '3A0G2+zJ3luLnlC44+Xe5HGw/9RWJNoyF2XZACvev20=', 'Ezequiel', 'Diaz', 'diazezequiel777@gmail.com', 1),
(36274589, '3A0G2+zJ3luLnlC44+Xe5HGw/9RWJNoyF2XZACvev20=', 'María', 'Gómez', 'maria.gomez@example.com', 0),
(37321456, '3A0G2+zJ3luLnlC44+Xe5HGw/9RWJNoyF2XZACvev20=', 'Lucía', 'Ramírez', 'lucia.ramirez@example.com', 1),
(38127465, '3A0G2+zJ3luLnlC44+Xe5HGw/9RWJNoyF2XZACvev20=', 'Luis', 'García', 'luis.garcia@example.com', 1),
(39985674, '3A0G2+zJ3luLnlC44+Xe5HGw/9RWJNoyF2XZACvev20=', 'Laura', 'Martínez', 'laura.martinez@example.com', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `aplicacion`
--

CREATE TABLE `aplicacion` (
  `Id` int(11) NOT NULL,
  `LoteProveedorId` int(11) NOT NULL,
  `AgenteId` int(11) NOT NULL,
  `Dosis` int(11) NOT NULL,
  `Estado` enum('Pendiente','Aplicada','Cancelada') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `aplicacion`
--

INSERT INTO `aplicacion` (`Id`, `LoteProveedorId`, `AgenteId`, `Dosis`, `Estado`) VALUES
(11, 1, 34229421, 1, 'Aplicada'),
(12, 2, 23948512, 1, 'Pendiente'),
(13, 3, 24367895, 2, 'Aplicada'),
(14, 4, 28765432, 3, 'Cancelada'),
(15, 5, 39985674, 4, 'Aplicada'),
(16, 6, 37321456, 5, 'Pendiente'),
(17, 7, 31098765, 6, 'Aplicada'),
(18, 7, 34229421, 7, 'Pendiente'),
(19, 8, 34229421, 8, 'Aplicada'),
(20, 9, 23948512, 9, 'Cancelada');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `laboratorio`
--

CREATE TABLE `laboratorio` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(50) NOT NULL,
  `Pais` varchar(30) NOT NULL,
  `Email` varchar(70) NOT NULL,
  `Telefono` varchar(20) NOT NULL,
  `Direccion` varchar(100) NOT NULL,
  `Estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `laboratorio`
--

INSERT INTO `laboratorio` (`Id`, `Nombre`, `Pais`, `Email`, `Telefono`, `Direccion`, `Estado`) VALUES
(1, 'Pfizer', 'EEUU', 'pfizereeuu@gmail.com', '12345678', 'Av. Los Alamos 1234', 1),
(2, 'Bago', 'Argentina', 'bagoarg@gmail.com', '12345679', 'Av. Siempreviva 4321', 1),
(3, 'Bayer', 'Argentina', 'bayerarg@gmail.com', '12345677', 'Av. San Luis 3241', 1),
(4, 'Laboratorios Puntanos', 'Argentina', 'labpunarg@gmail.com', '12345676', 'Av. Serrania Puntana 2314', 1),
(5, 'Lab Pharma', 'Argentina', 'contacto@labpharma.com', '+54 11 4567 1234', 'Av. Corrientes 1234, Buenos Aires', 1),
(6, 'BioTech', 'Brasil', 'info@biotech.com.br', '+55 21 9876 5432', 'Rua das Flores 567, Rio de Janeiro', 1),
(7, 'HealthCorp', 'Chile', 'ventas@healthcorp.cl', '+56 2 3456 7890', 'Calle O\'Higgins 234, Santiago', 1),
(8, 'MediLife', 'México', 'medilife@correo.mx', '+52 55 1234 5678', 'Paseo de la Reforma 789, Ciudad de México', 1),
(9, 'PharmaPlus', 'Colombia', 'contacto@pharmaplus.co', '+57 1 3456 7890', 'Carrera 7 #89-45, Bogotá', 0),
(10, 'GlobalMed', 'Perú', 'globalmed@peru.com', '+51 1 2345 6789', 'Av. Larco 456, Lima', 1),
(11, 'CureLabs', 'España', 'info@curelabs.es', '+34 91 456 7890', 'Calle Gran Vía 23, Madrid', 1),
(12, 'Salud XXI', 'Uruguay', 'contacto@saludxxi.uy', '+598 2 2345 6789', '18 de Julio 456, Montevideo', 0),
(13, 'InnovaPharm', 'Argentina', 'innova@pharm.com.ar', '+54 11 6789 1234', 'Av. Libertador 987, Buenos Aires', 1),
(14, 'BioLabs', 'Chile', 'contacto@biolabs.cl', '+56 2 5678 1234', 'Calle Las Palmeras 789, Santiago', 1),
(15, 'PharmaCorp', 'México', 'ventas@pharmacorp.mx', '+52 55 6789 1234', 'Insurgentes Sur 456, Ciudad de México', 1),
(16, 'MedSolutions', 'Brasil', 'info@medsolutions.com.br', '+55 11 2345 6789', 'Av. Paulista 123, São Paulo', 1),
(17, 'Salud Global', 'Colombia', 'contacto@saludglobal.co', '+57 1 5678 1234', 'Calle 100 #23-45, Bogotá', 1),
(18, 'VitaLab', 'Perú', 'vitalab@peru.com', '+51 1 5678 4321', 'Av. Grau 567, Lima', 0),
(19, 'Sanitas', 'España', 'info@sanitas.es', '+34 93 2345 6789', 'Paseo de Gracia 123, Barcelona', 1),
(20, 'MediPlus', 'Uruguay', 'contacto@mediplus.uy', '+598 2 6789 1234', 'Av. Italia 789, Montevideo', 1),
(21, 'PharmaHealth', 'Argentina', 'ventas@pharmahealth.com.ar', '+54 341 2345 6789', 'Calle Córdoba 456, Rosario', 1),
(22, 'CuraMed', 'Chile', 'info@curamed.cl', '+56 2 6789 1234', 'Av. Providencia 1234, Santiago', 1),
(23, 'Salud Integral', 'México', 'ventas@saludintegral.mx', '+52 33 1234 5678', 'Av. Vallarta 456, Guadalajara', 1),
(24, 'BioHealth', 'Brasil', 'info@biohealth.com.br', '+55 21 3456 7890', 'Rua da Saúde 123, Rio de Janeiro', 0),
(25, 'PharmaMed', 'España', 'contacto@pharmamed.es', '+34 95 5678 1234', 'Calle Sierpes 456, Sevilla', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `loteproveedor`
--

CREATE TABLE `loteproveedor` (
  `Id` int(11) NOT NULL,
  `NumeroDeLote` int(11) NOT NULL,
  `LaboratorioId` int(11) NOT NULL,
  `TipoDeVacunaId` int(11) NOT NULL,
  `CantidadDeVacunas` int(11) NOT NULL,
  `FechaDeVencimiento` date NOT NULL,
  `Estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `loteproveedor`
--

INSERT INTO `loteproveedor` (`Id`, `NumeroDeLote`, `LaboratorioId`, `TipoDeVacunaId`, `CantidadDeVacunas`, `FechaDeVencimiento`, `Estado`) VALUES
(1, 1001, 1, 1, 5000, '2029-09-01', 1),
(2, 1002, 2, 2, 7500, '2030-09-01', 1),
(3, 1003, 3, 3, 6000, '2031-09-01', 1),
(4, 1004, 4, 4, 8000, '2029-09-01', 1),
(5, 1005, 5, 5, 4500, '2030-09-01', 1),
(6, 1006, 6, 6, 5500, '2032-09-01', 1),
(7, 1007, 7, 7, 6200, '2029-09-01', 1),
(8, 1008, 8, 8, 5300, '2031-09-01', 1),
(9, 1009, 9, 9, 7100, '2029-09-01', 1),
(10, 1010, 10, 10, 7600, '2030-09-01', 1),
(11, 1011, 11, 11, 6400, '2031-09-01', 1),
(12, 1012, 12, 12, 8300, '2032-09-01', 1),
(13, 1013, 13, 13, 5000, '2029-09-01', 1),
(14, 1014, 14, 14, 7800, '2030-09-01', 1),
(15, 1015, 15, 15, 6200, '2031-09-01', 1),
(16, 1016, 16, 16, 6800, '2032-09-01', 1),
(17, 1017, 17, 17, 5600, '2029-09-01', 1),
(18, 1018, 18, 18, 5900, '2030-09-01', 1),
(19, 1019, 19, 19, 7400, '2031-09-01', 1),
(20, 1020, 20, 20, 6800, '2032-09-01', 1),
(21, 1021, 21, 1, 5000, '2029-09-01', 1),
(22, 1022, 22, 2, 6000, '2030-09-01', 1),
(23, 1023, 23, 3, 5500, '2031-09-01', 1),
(24, 1024, 24, 4, 6500, '2032-09-01', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `paciente`
--

CREATE TABLE `paciente` (
  `Id` int(11) NOT NULL,
  `DNI` varchar(15) NOT NULL,
  `Nombre` varchar(50) NOT NULL,
  `Apellido` varchar(50) NOT NULL,
  `FechaDeNacimiento` date NOT NULL,
  `Genero` enum('Masculino','Femenino','Otro') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `paciente`
--

INSERT INTO `paciente` (`Id`, `DNI`, `Nombre`, `Apellido`, `FechaDeNacimiento`, `Genero`) VALUES
(1, '12345678', 'Juan', 'Pérez', '2007-05-15', 'Masculino'),
(2, '87654321', 'María', 'Gómez', '2008-09-20', 'Femenino'),
(3, '34567890', 'Carlos', 'López', '2009-03-25', 'Masculino'),
(4, '56789012', 'Laura', 'Martínez', '2010-12-10', 'Femenino'),
(5, '23456789', 'Ana', 'Fernández', '2006-07-30', 'Femenino'),
(6, '67890123', 'Luis', 'García', '2009-01-05', 'Masculino'),
(7, '78901234', 'Sofía', 'Rodríguez', '2010-11-11', 'Femenino'),
(8, '89012345', 'Miguel', 'Sánchez', '2008-06-14', 'Masculino'),
(9, '90123456', 'Lucía', 'Ramírez', '2007-04-20', 'Femenino'),
(10, '01234567', 'Diego', 'Silva', '2011-08-08', 'Masculino'),
(11, '23456780', 'Valeria', 'Mendoza', '2006-02-19', 'Femenino'),
(12, '34567801', 'Fernando', 'Castro', '2010-03-12', 'Masculino'),
(13, '45678902', 'Gabriela', 'Morales', '2008-10-25', 'Femenino'),
(14, '56789013', 'Martín', 'Rojas', '2007-05-18', 'Masculino'),
(15, '67890124', 'Camila', 'Ortiz', '2011-09-09', 'Femenino'),
(16, '78901235', 'Javier', 'Guzmán', '2009-07-21', 'Masculino'),
(17, '89012346', 'Paula', 'Flores', '2010-11-30', 'Femenino'),
(18, '90123457', 'Andrés', 'Pereira', '2006-01-22', 'Masculino'),
(19, '01234568', 'Natalia', 'Suárez', '2009-12-15', 'Femenino'),
(20, '12345679', 'Ricardo', 'Herrera', '2011-04-05', 'Masculino'),
(21, '23456781', 'Elena', 'Vega', '2008-02-28', 'Femenino'),
(22, '34567802', 'Hernán', 'Molina', '2010-09-14', 'Masculino'),
(23, '45678903', 'Carla', 'Muñoz', '2011-10-17', 'Femenino'),
(24, '56789014', 'Sebastián', 'Paredes', '2007-03-03', 'Masculino'),
(25, '48456723', 'Enzo', 'Miranda', '2010-05-15', 'Masculino');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipodevacuna`
--

CREATE TABLE `tipodevacuna` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(50) NOT NULL,
  `Descripcion` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipodevacuna`
--

INSERT INTO `tipodevacuna` (`Id`, `Nombre`, `Descripcion`) VALUES
(1, 'BCG', 'Vacuna contra la tuberculosis.'),
(2, 'Hepatitis B', 'Vacuna contra la hepatitis B.'),
(3, 'Pentavalente', 'Protege contra difteria, tétanos, tos ferina, hepatitis B, e infecciones invasivas por Hib.'),
(4, 'Polio Oral', 'Vacuna contra la poliomielitis.'),
(5, 'Polio Inactivada', 'Vacuna inactivada contra la poliomielitis.'),
(6, 'Rotavirus', 'Protege contra infecciones por rotavirus, que causan diarrea grave.'),
(7, 'Neumococo Conjugada', 'Previene infecciones por neumococo, incluyendo neumonía, meningitis y sepsis.'),
(8, 'Influenza', 'Protege contra la gripe estacional.'),
(9, 'SRP (Triple viral)', 'Vacuna contra sarampión, rubéola y paperas.'),
(10, 'DPT', 'Vacuna contra difteria, tétanos y tos ferina.'),
(11, 'Hepatitis A', 'Vacuna contra la hepatitis A.'),
(12, 'Varicela', 'Protege contra la varicela.'),
(13, 'VPH', 'Vacuna contra el virus del papiloma humano.'),
(14, 'Meningococo', 'Previene infecciones por meningococo, incluyendo meningitis.'),
(15, 'Tétanos', 'Vacuna contra el tétanos.'),
(16, 'Fiebre Amarilla', 'Vacuna contra la fiebre amarilla.'),
(17, 'Antirrábica', 'Vacuna contra la rabia.'),
(18, 'COVID-19', 'Vacuna para prevenir la infección por el virus SARS-CoV-2.'),
(19, 'Zoster', 'Vacuna contra el herpes zóster (culebrilla).'),
(20, 'Dengue', 'Vacuna contra el virus del dengue.');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `turno`
--

CREATE TABLE `turno` (
  `Id` int(11) NOT NULL,
  `PacienteId` int(11) NOT NULL,
  `TipoDeVacunaId` int(11) NOT NULL,
  `TutorId` int(11) NOT NULL,
  `AgenteId` int(11) NOT NULL,
  `AplicacionId` int(11) DEFAULT NULL,
  `Cita` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tutor`
--

CREATE TABLE `tutor` (
  `Id` int(11) NOT NULL,
  `DNI` varchar(15) NOT NULL,
  `Nombre` varchar(50) NOT NULL,
  `Apellido` varchar(50) NOT NULL,
  `Telefono` varchar(20) NOT NULL,
  `Email` varchar(70) NOT NULL,
  `Relacion` enum('Madre','Padre','Tutor','Otro') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tutor`
--

INSERT INTO `tutor` (`Id`, `DNI`, `Nombre`, `Apellido`, `Telefono`, `Email`, `Relacion`) VALUES
(1, '12345678', 'María', 'Pérez', '+54 11 2345 6789', 'maria.perez@example.com', 'Madre'),
(2, '23456789', 'Juan', 'Gómez', '+54 11 3456 7890', 'juan.gomez@example.com', 'Padre'),
(3, '34567890', 'Ana', 'López', '+54 11 4567 8901', 'ana.lopez@example.com', 'Madre'),
(4, '45678901', 'Carlos', 'Martínez', '+54 11 5678 9012', 'carlos.martinez@example.com', 'Otro'),
(5, '56789012', 'Laura', 'Fernández', '+54 11 6789 0123', 'laura.fernandez@example.com', 'Madre'),
(6, '67890123', 'Miguel', 'García', '+54 11 7890 1234', 'miguel.garcia@example.com', 'Padre'),
(7, '78901234', 'Sofía', 'Rodríguez', '+54 11 8901 2345', 'sofia.rodriguez@example.com', 'Otro'),
(8, '89012345', 'Luis', 'Sánchez', '+54 11 9012 3456', 'luis.sanchez@example.com', 'Padre'),
(9, '90123456', 'Elena', 'Ramírez', '+54 11 0123 4567', 'elena.ramirez@example.com', 'Otro'),
(10, '01234567', 'Fernando', 'Silva', '+54 11 1234 5678', 'fernando.silva@example.com', 'Padre');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `agente`
--
ALTER TABLE `agente`
  ADD PRIMARY KEY (`Matricula`);

--
-- Indices de la tabla `aplicacion`
--
ALTER TABLE `aplicacion`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `AgenteId` (`AgenteId`),
  ADD KEY `LoteProveedorId` (`LoteProveedorId`);

--
-- Indices de la tabla `laboratorio`
--
ALTER TABLE `laboratorio`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `loteproveedor`
--
ALTER TABLE `loteproveedor`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `NumeroDeLote` (`NumeroDeLote`,`LaboratorioId`),
  ADD KEY `LaboratorioId` (`LaboratorioId`),
  ADD KEY `TipoDeVacunaId` (`TipoDeVacunaId`);

--
-- Indices de la tabla `paciente`
--
ALTER TABLE `paciente`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `tipodevacuna`
--
ALTER TABLE `tipodevacuna`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `turno`
--
ALTER TABLE `turno`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `AgenteId` (`AgenteId`),
  ADD KEY `PacienteId` (`PacienteId`),
  ADD KEY `TutorId` (`TutorId`),
  ADD KEY `AplicacionId` (`AplicacionId`),
  ADD KEY `TipoDeVacunaId` (`TipoDeVacunaId`);

--
-- Indices de la tabla `tutor`
--
ALTER TABLE `tutor`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `aplicacion`
--
ALTER TABLE `aplicacion`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT de la tabla `laboratorio`
--
ALTER TABLE `laboratorio`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT de la tabla `loteproveedor`
--
ALTER TABLE `loteproveedor`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT de la tabla `paciente`
--
ALTER TABLE `paciente`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT de la tabla `tipodevacuna`
--
ALTER TABLE `tipodevacuna`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT de la tabla `turno`
--
ALTER TABLE `turno`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `tutor`
--
ALTER TABLE `tutor`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `aplicacion`
--
ALTER TABLE `aplicacion`
  ADD CONSTRAINT `aplicacion_ibfk_1` FOREIGN KEY (`AgenteId`) REFERENCES `agente` (`Matricula`),
  ADD CONSTRAINT `aplicacion_ibfk_2` FOREIGN KEY (`LoteProveedorId`) REFERENCES `loteproveedor` (`Id`);

--
-- Filtros para la tabla `loteproveedor`
--
ALTER TABLE `loteproveedor`
  ADD CONSTRAINT `loteproveedor_ibfk_1` FOREIGN KEY (`LaboratorioId`) REFERENCES `laboratorio` (`Id`),
  ADD CONSTRAINT `loteproveedor_ibfk_2` FOREIGN KEY (`TipoDeVacunaId`) REFERENCES `tipodevacuna` (`Id`);

--
-- Filtros para la tabla `turno`
--
ALTER TABLE `turno`
  ADD CONSTRAINT `turno_ibfk_1` FOREIGN KEY (`AgenteId`) REFERENCES `agente` (`Matricula`),
  ADD CONSTRAINT `turno_ibfk_2` FOREIGN KEY (`PacienteId`) REFERENCES `paciente` (`Id`),
  ADD CONSTRAINT `turno_ibfk_3` FOREIGN KEY (`TutorId`) REFERENCES `tutor` (`Id`),
  ADD CONSTRAINT `turno_ibfk_4` FOREIGN KEY (`AplicacionId`) REFERENCES `aplicacion` (`Id`),
  ADD CONSTRAINT `turno_ibfk_5` FOREIGN KEY (`TipoDeVacunaId`) REFERENCES `tipodevacuna` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
