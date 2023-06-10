import React, { useContext, useState, useEffect } from "react";
import { Alert, AlertTitle, Box, Button, Avatar } from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
import NavBar from "../NavBar/NavBar";
import { getAllProducts } from "../../services/ProductService";
import CartContext from "../../contexts/cart-context";
import Cart from "./Cart/Cart";

const Shop = () => {
  const cartCtx = useContext(CartContext);

  const exceptionRead = (value) => value.split(":")[1].split("at")[0];
  const [products, setProducts] = useState([]);
  const [openCart, setOpenCart] = useState(false);
  const [alert, setAlert] = useState({
    message: "",
    severity: "success",
  });

  const img = "data:image/*;base64,";
  const imgUrl = process.env.PUBLIC_URL + "/product.jpg";

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await getAllProducts();
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
  }, []);

  const openCartHandler = () => {
    setOpenCart(true);
  };

  const closeCartHandler = () => {
    setOpenCart(false);
  };

  const handleAddProduct = (id, name, price) => {
    console.log(id, name, price);
    cartCtx.addItem({
      id: id,
      name: name,
      amount: 1,
      price: price,
    });
  };

  const columns = [
    {
      field: "id",
      headerName: "ID",
      width: 80,
      type: "number",
      headerAlign: "center",
    },
    {
      field: "image",
      headerName: "Image",
      width: 250,
      sortable: false,
      headerAlign: "center",
      renderCell: (params) => {
        const { image} = params.row;
        return (
          <div>
            <Avatar
              src={image !== "" ? img + image : imgUrl}
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
      field: "",
      headerName: "",
      width: 250,
      sortable: false,
      disableColumnMenu: true,
      renderCell: (params) => {
        const { id, name, price } = params.row;
        return (
          <div>
            <Button
              variant="contained"
              color="primary"
              onClick={() => handleAddProduct(id, name, price)}
            >
              Add to cart
            </Button>
          </div>
        );
      },
    },
    {
      field: 'cartButton',
      headerName: '',
      width: 150,
      sortable: false,
      disableColumnMenu: true,
      headerAlign: 'right',
      renderHeader: () => (
        <Button
          variant="contained"
          color="primary"
          onClick={() => openCartHandler()}
          sx={{ m: 1 }}
        >
          My Cart
        </Button>
      ),
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
      <Cart open={openCart} onClose={closeCartHandler} />
    </>
  );
};

export default Shop;
