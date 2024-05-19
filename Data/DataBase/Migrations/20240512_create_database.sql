CREATE TABLE products (
    cod_sku_pk SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    description TEXT,
    date_update DATE NOT NULL DEFAULT CURRENT_DATE
);