import React, { useState, useEffect, useContext } from "react";
import AuthContext from "../../contexts/auth-context";
import PropTypes from "prop-types";
import {
  Box,
  Collapse,
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
  Paper,
  Alert,
  AlertTitle
} from "@mui/material";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import KeyboardArrowUpIcon from "@mui/icons-material/KeyboardArrowUp";
import {
  getAllOrders,
  getCustomerDeliveredOrders,
  getCustomerInProgressOrders,
  getSalesmanDeliveredOrders,
  getSalesmanInProgressOrders,
} from "../../services/OrderService";
import NavBar from "../NavBar/NavBar";

function Row({ row }) {
  const [open, setOpen] = useState(false);

  return (
    <React.Fragment>
      <TableRow sx={{ "& > *": { borderBottom: "unset" } }}>
        <TableCell>
          <IconButton
            aria-label="expand row"
            size="small"
            onClick={() => setOpen(!open)}
          >
            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
          </IconButton>
        </TableCell>
        <TableCell component="th" scope="row">
          {row.id}
        </TableCell>
        <TableCell align="center">{row.comment}</TableCell>
        <TableCell align="center">{row.address}</TableCell>
        <TableCell align="center">{row.price}</TableCell>
        <TableCell align="center">{row.orderTime.split(".")[0]}</TableCell>
        <TableCell align="center">{row.deliveryTime.split(".")[0]}</TableCell>
        <TableCell align="center">{row.status}</TableCell>
      </TableRow>
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={8}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box sx={{ margin: 1 }}>
              <Typography variant="h6" gutterBottom component="div">
                Details
              </Typography>
              <Table size="small" aria-label="purchases">
                <TableHead>
                  <TableRow>
                    <TableCell align="center">Name</TableCell>
                    <TableCell align="center">Description</TableCell>
                    <TableCell align="center">Price</TableCell>
                    <TableCell align="center">Amount</TableCell>
                    <TableCell align="right">Total price (RSD)</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {row.orderProducts.map((orderProduct) => (
                    <TableRow key={orderProduct.id}>
                      <TableCell align="center">
                        {orderProduct.product.name}
                      </TableCell>
                      <TableCell align="center">
                        {orderProduct.product.description}
                      </TableCell>
                      <TableCell align="center">
                        {orderProduct.product.price}
                      </TableCell>
                      <TableCell align="center">
                        {orderProduct.amount}
                      </TableCell>
                      <TableCell align="right">
                        {orderProduct.amount * orderProduct.product.price}
                      </TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
}

Row.propTypes = {
  row: PropTypes.shape({
    id: PropTypes.number.isRequired,
    comment: PropTypes.string.isRequired,
    address: PropTypes.string.isRequired,
    price: PropTypes.number.isRequired,
    orderTime: PropTypes.string.isRequired,
    deliveryTime: PropTypes.string.isRequired,
    status: PropTypes.number.isRequired,
    orderProducts: PropTypes.arrayOf(
      PropTypes.shape({
        id: PropTypes.number.isRequired,
        amount: PropTypes.number.isRequired,
        product: PropTypes.shape({
          id: PropTypes.number.isRequired,
          name: PropTypes.string.isRequired,
          price: PropTypes.number.isRequired,
          amount: PropTypes.number.isRequired,
          description: PropTypes.string.isRequired,
          image: PropTypes.string, // Adjust the type if necessary
        }).isRequired,
      })
    ).isRequired,
    userId: PropTypes.number.isRequired,
    deliveryPrice: PropTypes.number.isRequired,
  }).isRequired,
};

const Orders = () => {
  const exceptionRead = (value) => value.split(":")[1].split("at")[0];
  const [data, setData] = useState([]);
  const [alert, setAlert] = useState({
    message: "",
    severity: "success",
  });

  const authCtx = useContext(AuthContext);
  const role = authCtx.role;

  const isAdmin = role === "ADMINISTRATOR";
  const isCustomer = role === "CUSTOMER";
  const isSalesman = role === "SALESMAN";

  useEffect(() => {
    const fetchData = async () => {
      if (isAdmin) {
        try {
          const response = await getAllOrders();
          console.log("ovaj ispis", response.data);
          setData(response.data);
        } catch (error) {
          setAlert({
            message: exceptionRead(error.response.data),
            severity: "error",
          });
          return;
        }
      }
      if (isSalesman) {
        try {
          const responseInProgress = await getSalesmanInProgressOrders();
          console.log("ovaj ispis", responseInProgress.data);
          setData(responseInProgress.data);
          const responseDelivered = await getSalesmanDeliveredOrders();
          setData((prevData) => ({
            ...prevData,
            ...responseDelivered.data,
          }));
        } catch (error) {
          setAlert({
            message: exceptionRead(error.response.data),
            severity: "error",
          });
          return;
        }
      }

      if (isCustomer) {
        try {
          const responseInProgress = await getCustomerInProgressOrders();
          console.log("ovaj ispis", responseInProgress.data);
          setData(responseInProgress.data);
          const responseDelivered = await getCustomerDeliveredOrders();
          setData((prevData) => ({
            ...prevData,
            ...responseDelivered.data,
          }));
        } catch (error) {
          setAlert({
            message: exceptionRead(error.response.data),
            severity: "error",
          });
          return;
        }
      }
    };
    fetchData();
  }, []);

  return (
    <>
      {alert.message !== "" && (
        <Alert
        sx={{  
          position: 'fixed',
          top: 0,
          left: 0,
          right: 0,
          width: 'auto'}}
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
        <div
          style={{
            position: "fixed",
            top: 65,
            width: "100%",
            backgroundColor: "blue",
          }}
        >
          <TableContainer component={Paper} style={{ background: "#243b55" }}>
            <Table aria-label="collapsible table">
              <TableHead>
                <TableRow>
                  <TableCell />
                  <TableCell>Id</TableCell>
                  <TableCell align="center">Comment</TableCell>
                  <TableCell align="center">Address</TableCell>
                  <TableCell align="center">Price</TableCell>
                  <TableCell align="center">Order Time</TableCell>
                  <TableCell align="center">Delivery Time</TableCell>
                  <TableCell align="center">Status</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {data.map((row) => (
                  <Row key={row.id} row={row} />
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </div>
      </Box>
    </>
  );
};

Orders.propTypes = {
  data: PropTypes.arrayOf(PropTypes.object).isRequired,
};

export default Orders;
