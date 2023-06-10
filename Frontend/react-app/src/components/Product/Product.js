import React, { useState, useEffect } from "react";
import NewProduct from "./NewProduct/NewProduct";
import {
  Alert,
  AlertTitle,
  Box,
  Button,
  Avatar,

} from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
import NavBar from "../NavBar/NavBar";
import {
  deleteProduct,
  getMyProducts,
  getProductById,
} from "../../services/ProductService";
import UpdateProduct from "./UpdateProduct/UpdateProduct.js";

const Product = () => {
  const exceptionRead = (value) => value.split(":")[1].split("at")[0];
  const [newProductOpen, setNewProductOpen] = useState(false);
  const [updateProductOpen, setUpdateProductOpen] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState(null);
  const [products, setProducts] = useState([]);
  const [change, setChange] = useState(false);
  const [alert, setAlert] = useState({
    message: "",
    severity: "success",
  });
  const img = "data:image/*;base64,";
  const imgUrl = process.env.PUBLIC_URL + "/product.jpg";

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await getMyProducts();
        setProducts(response.data);
        console.log(response.data);
      } catch (error) {
        setAlert({
          message: exceptionRead(error.response.data),
          severity: "error",
        });
        return;
      }
    };

    fetchData();
  }, [change]);

  const handleAddProduct = () => {
    setNewProductOpen(true);
  };

  const handleCloseNewProduct = () => {
    setNewProductOpen(false);
    setChange(!change);
    console.log('usao je ovdje laze');
  };

  const handleUpdateProduct = (id) => {
    setUpdateProductOpen(true);
    const fetchData = async () => {
      try {
        const response = await getProductById(id);
        console.log(response);
        setSelectedProduct(response.data);
        console.log(response.data);
      } catch (error) {
        setAlert({
          message: exceptionRead(error.response.data),
          severity: "error",
        });
        return;
      }
    };

    fetchData();
  };

  const handleCloseUpdateProduct = () => {
    setUpdateProductOpen(false);
  };

  const handleDeleteProduct = async (id) => {
    console.log('ID', id);
    try {
      const response = await deleteProduct(id);
      console.log(response);
      setChange(!change);
    } catch (error) {
      setAlert({
        message: error.response.data,
        severity: "error",
      });
      return;
    }
  };

  const columns = [
    { field: "id", headerName: "ID", width: 80, type: "number" },
    {
      field: "image",
      headerName: "Image",
      width: 250,
      sortable: false,
      renderCell: (params) => {
        const { Image } = params.row;
        return (
          <div>
            <Avatar
              src={Image !== "" ? img + Image : imgUrl}
              style={{ width: "100%", height: "auto" }}
            ></Avatar>
          </div>
        );
      },
    },
    { field: "name", headerName: "Name", width: 130 },
    { field: "description", headerName: "Description", width: 170 },
    { field: "amount", headerName: "Amount", width: 100 },
    { field: "price", headerName: "Price", width: 100 },
    {
      field: (
        <Button
          variant="contained"
          color="primary"
          className="addButton"
          onClick={() => handleAddProduct()}
          sx={{ mt: 0 }}
        >
          Add New Product
        </Button>
      ),
      width: 230,
      sortable: false,
      renderCell: (params) => {
        const { id } = params.row;
        return (
          <div>
            <Button
              variant="contained"
              color="primary"
              onClick={() => handleUpdateProduct(id)}
            >
              Change
            </Button>
            <Button
              variant="contained"
              color="secondary"
              onClick={() => handleDeleteProduct(id)}
            >
              Delete
            </Button>
          </div>
        );
      },
    },
  ];

  return (
    <>
      {alert.message !== "" && (
        <Alert
          sx={{
            position: "fixed",
            top: 0,
            left: 0,
            right: 0,
            width: "auto",
          }}
          onClose={() => setAlert({ message: "", severity: "success" })}
        >
          <AlertTitle>
            {alert.severity.charAt(0).toUpperCase() + alert.severity.slice(1)}
          </AlertTitle>
          {alert.message}
        </Alert>
      )}
      <NavBar />
      <Box
        sx={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          height: "100vh",
          backgroundColor: "#243b55",
        }}
      >
        <NewProduct open={newProductOpen} onClose={handleCloseNewProduct} />
        {selectedProduct && (
          <UpdateProduct
            open={updateProductOpen}
            onClose={handleCloseUpdateProduct}
            product={selectedProduct}
          />
        )}
        <DataGrid
          rows={products}
          columns={columns}
          rowHeight={200}
          sx={{ color: "white", textAlign: "center" }}
          initialState={{
            pagination: {
              paginationModel: { page: 0, pageSize: 5 },
            },
          }}
          pageSizeOptions={[5, 10]}
        />
      </Box>
    </>
  );
};

export default Product;
