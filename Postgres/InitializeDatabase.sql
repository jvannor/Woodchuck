-- --------------------------------------------------------------------------------
-- Database: WoodchuckDb
-- --------------------------------------------------------------------------------

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', 'public', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

-- DROP DATABASE "WoodchuckDb";
CREATE DATABASE "WoodchuckDb"
    WITH TEMPLATE = template0
    ENCODING = 'UTF8'
    LC_COLLATE = 'en_US.utf8'
    LC_CTYPE = 'en_US.utf8';

ALTER DATABASE "WoodchuckDb" OWNER TO postgres;

\connect -reuse-previous=on "dbname='WoodchuckDb'"

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', 'public', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

-- --------------------------------------------------------------------------------
-- Table: "CameraSettings"
-- --------------------------------------------------------------------------------

CREATE SEQUENCE "CameraSettings_Id_seq"
    start 1
    increment 1;

CREATE TABLE "CameraSettings"
(
    "Id" integer NOT NULL DEFAULT nextval('"CameraSettings_Id_seq"'::regclass),
    "Name" character varying(128) COLLATE pg_catalog."default" NOT NULL,
    "URI" character varying(1024) COLLATE pg_catalog."default" NOT NULL,
    "User" character varying(128) COLLATE pg_catalog."default",
    "Password" character varying(256) COLLATE pg_catalog."default",
    CONSTRAINT "CameraSettings_pkey" PRIMARY KEY ("Id")
);

ALTER TABLE "CameraSettings"
    OWNER to postgres;

-- --------------------------------------------------------------------------------
-- Table: "MonitorSettings"
-- --------------------------------------------------------------------------------

CREATE SEQUENCE "MonitorSettings_Id_seq"
    start 1
    increment 1;

CREATE TABLE "MonitorSettings"
(
    "Id" integer NOT NULL DEFAULT nextval('"MonitorSettings_Id_seq"'::regclass),
    "Key" character varying(64) COLLATE pg_catalog."default" NOT NULL,
    "Value" character varying(1024) COLLATE pg_catalog."default",
    CONSTRAINT "MonitorSettings_pkey" PRIMARY KEY ("Id")
);

ALTER TABLE "MonitorSettings"
    OWNER to postgres;

-- --------------------------------------------------------------------------------
-- Table: "NotificationSettings"
-- --------------------------------------------------------------------------------

CREATE SEQUENCE "NotificationSettings_Id_seq"
    start 1
    increment 1;

CREATE TABLE "NotificationSettings"
(
    "Id" integer NOT NULL DEFAULT nextval('"NotificationSettings_Id_seq"'::regclass),
    "Name" character varying(256) COLLATE pg_catalog."default" NOT NULL,
    "Telephone" character varying(24) COLLATE pg_catalog."default" NOT NULL,
    "Enabled" boolean NOT NULL,
    CONSTRAINT "NotificationSettings_pkey" PRIMARY KEY ("Id")
);

ALTER TABLE "NotificationSettings"
    OWNER to postgres;

-- --------------------------------------------------------------------------------
-- Table: "WorkerSettings"
-- --------------------------------------------------------------------------------

CREATE SEQUENCE "WorkerSettings_Id_seq"
    start 1
    increment 1;

CREATE TABLE "WorkerSettings"
(
    "Id" integer NOT NULL DEFAULT nextval('"WorkerSettings_Id_seq"'::regclass),
    "Key" character varying(64) COLLATE pg_catalog."default" NOT NULL,
    "Value" character varying(1024) COLLATE pg_catalog."default",
    CONSTRAINT "WorkerSettings_pkey" PRIMARY KEY ("Id")
);

ALTER TABLE "WorkerSettings"
    OWNER to postgres;

-- --------------------------------------------------------------------------------
-- Data for Name: MonitorSettings; Type: TABLE DATA; Schema: public; Owner: -
-- --------------------------------------------------------------------------------

INSERT INTO "MonitorSettings" VALUES (1, 'RabbitMQ-HostName', 'rabbitmq');
INSERT INTO "MonitorSettings" VALUES (2, 'RabbitMQ-ExchangeName', 'doorbell-events');
INSERT INTO "MonitorSettings" VALUES (3, 'RabbitMQ-QueueName', 'worker-queue');
INSERT INTO "MonitorSettings" VALUES (4, 'RabbitMQ-RoutingKey', 'worker');

-- --------------------------------------------------------------------------------
-- Data for Name: WorkerSettings; Type: TABLE DATA; Schema: public; Owner: -
-- --------------------------------------------------------------------------------

INSERT INTO "WorkerSettings" VALUES (1, 'RabbitMQ-HostName', 'rabbitmq');
INSERT INTO "WorkerSettings" VALUES (2, 'RabbitMQ-ExchangeName', 'doorbell-events');
INSERT INTO "WorkerSettings" VALUES (3, 'RabbitMQ-QueueName', 'worker-queue');
INSERT INTO "WorkerSettings" VALUES (4, 'RabbitMQ-RoutingKey', 'worker');
INSERT INTO "WorkerSettings" VALUES (5, 'Twilio_User', 'TBD');
INSERT INTO "WorkerSettings" VALUES (6, 'Twilio_Password', 'TBD');
INSERT INTO "WorkerSettings" VALUES (7, 'Twilio_Telephone', 'TBD');
INSERT INTO "WorkerSettings" VALUES (8, 'Storage_Uri', 'TBD');
