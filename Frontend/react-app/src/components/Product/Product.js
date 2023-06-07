import React, { useState, useEffect } from "react";
import NewProduct from "./NewProduct/NewProduct";
import {
  Alert,
  AlertTitle,
  Typography,
  Grid,
  Box,
  Paper,
  Button,
  Avatar,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
} from "@mui/material";
import NavBar from "../NavBar/NavBar";
import { getMyProducts } from "../../services/ProductService";

const Product = () => {
  const [products, setProducts] = useState([]);
  const [newProductOpen, setNewProductOpen] = useState(false);
  const img = "data:image/*;base64,";
  const imgUrl = process.env.PUBLIC_URL + "/product.jpg";
  console.log(imgUrl);
  const exceptionRead = (value) => value.split(":")[1].split("at")[0];
  const [alert, setAlert] = useState({
    message: "",
    severity: "success",
  });

  const handleAddProduct = () => {
    setNewProductOpen(true);
  };

  const handleCloseNewProduct = () => {
    setNewProductOpen(false);
    // const execute = async () => {
    //   await dispatch(getSellerArticlesAction());
    // };
    // execute();
  };

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
  }, []);

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
        <Grid container justifyContent="center" mt={4}>
          <Grid item xs={12} sm={8} md={6} lg={10} >
            <Paper sx={{ p: 2, display: "flex", flexDirection: "column", backgroundColor: '#141e30', color: 'white' }}>
              <Box
                sx={{
                  display: "flex",
                  justifyContent: "space-between",
                  alignItems: "center",
                }}
              >
                <Typography component="h1" variant="h5">
                  My Products
                </Typography>
                <Button
                  variant="contained"
                  color="primary"
                  className="addButton"
                  onClick={() => handleAddProduct()}
                  sx={{ mt: 0 }}
                >
                  Add New Product
                </Button>
                <NewProduct
                  open={newProductOpen}
                  onClose={handleCloseNewProduct}
                />
              </Box>
              <Table size="medium">
                <TableHead>
                  <TableRow>
                    <TableCell sx={{ fontSize: 20, fontWeight: "bold", color: 'white', textAlign: 'center' }}>
                      Image
                    </TableCell>
                    <TableCell sx={{ fontSize: 20, fontWeight: "bold", color: 'white', textAlign: 'center' }}>
                      Name
                    </TableCell>
                    <TableCell sx={{ fontSize: 20, fontWeight: "bold" , color: 'white', textAlign: 'center'}}>
                      Description
                    </TableCell>
                    <TableCell sx={{ fontSize: 20, fontWeight: "bold" , color: 'white', textAlign: 'center'}}>
                      Amount
                    </TableCell>
                    <TableCell sx={{ fontSize: 20, fontWeight: "bold" , color: 'white', textAlign: 'center'}}>
                      Price
                    </TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {products.map((product) => (
                    <TableRow key={product.id}>
                      <TableCell style={{ width: "20%", height: "20%" }}>
                        <Avatar
                          src={
                            product.Image !== "" ? img + product.Image : imgUrl
                          }
                          style={{ width: "100%", height: "auto" }}
                        ></Avatar>
                      </TableCell>
                      <TableCell sx={{color: 'white', textAlign: 'center'}}>{product.name}</TableCell>
                      <TableCell sx={{color: 'white', textAlign: 'center'}}>{product.description}</TableCell>
                      <TableCell sx={{color: 'white', textAlign: 'center'}}>{product.amount}</TableCell>
                      <TableCell sx={{color: 'white', textAlign: 'center'}}>{product.price}</TableCell>
                      <TableCell align="right">
                        <Button
                           variant="outlined"
                           color="primary"
                          className="approveButton"
                          //onClick={() => handleUpdateArticle(article)}
                        >
                          Update
                        </Button>
                        <Button
                          sx={{ ml: 2 }}
                          variant="outlined"
                          color="secondary"
                          //   onClick={() => handleDeleteArticle(article.id)}
                        >
                          Delete
                        </Button>
                        {/* {selectedArticle && (
                          <UpdateModal
                            open={modalOpen}
                            onClose={handleCloseModal}
                            article={selectedArticle}
                          />
                        )} */}
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </Paper>
          </Grid>
        </Grid>
      </Box>
    </>
  );
};

export default Product;
