import { React, useEffect, useState } from "react";
import {
  Box,
  TextField,
  Divider,
  Stack,
  Button,
  FormControl,
  Paper,
  Radio,
  FormControlLabel,
  RadioGroup,
  FormLabel,
} from "@mui/material/";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import Swal from "sweetalert2";
import PropTypes from "prop-types";
import WaitingStateModal from "./WaitingStateModal";
import CheckingStateModal from "./CheckingStateModal";
import InquiryStateModal from "./InquiryStateModal";
import ReadyToRepairStateModal from "./ReadyToRepairStateModal";
import RepairingStateModal from "./RepairingStateModal";
import ReadyStateModal from "./ReadyStateModal";
import TicketService from "../../../../services/ticket.service";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "80%",
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
  mb: 1,
  "& .MuiTextField-root": { m: 1 },
};

function CustomTabPanel(props) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      {value === index && (
        <Box sx={{ p: 3 }}>
          <Typography>{children}</Typography>
        </Box>
      )}
    </div>
  );
}

CustomTabPanel.propTypes = {
  children: PropTypes.node,
  index: PropTypes.number.isRequired,
  value: PropTypes.number.isRequired,
};


export default function ActionModal(props) {
  const [status] = useState(props.data.status)

  const [ticket, setTicket] = useState(null);
  const [isAdmin] = useState(true);

  useEffect(() => {
    TicketService.get(props.data.id).then((response) => {
      const result = response.data.Data.Data;
      setTicket(result);
      debugger;
    });
  }, []);

  const handleSubmit = () => {};

  if (ticket === null) {
    return "Loading";
  }
  if (status.level === 8) {
    Swal.fire("تیکت بسته شده و فرآیند آن به پایان رسیده");
    return;
  }
  return (
    <Modal
      open={props.open}
      onClose={props.onClose}
      aria-labelledby="modal-modal-title"
      aria-describedby="modal-modal-description"
    >
      <Box sx={style}>
        <Paper elevation={3} sx={{ p: 2 }}>
          <Typography id="modal-modal-title" variant="h5" component="h5">
            {status.headerText}
          </Typography>
          <Divider />
          <Box sx={{ m: 2, display: "flex", justifyContent: "space-between" }}>
            <Typography variant="h6">شماره تیکت: {ticket.Number}</Typography>
            <Typography variant="h6">
              تاریخ تیکت: {ticket.PersianDate}
            </Typography>
          </Box>
          <Divider />
          {isAdmin && (
            <>
              <Box
                sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}
              >
                <Typography variant="h6">
                  نام مشتری: {ticket.Customer.Person.Name}
                </Typography>
                <Typography
                  color={
                    ticket.Customer.Person.User.PhoneConfirmation ? "" : "error"
                  }
                  variant="h6"
                >
                  شماره تماس: {ticket.Customer.Person.PhoneNumber}
                </Typography>
                <Typography
                  color={
                    ticket.Customer.Person.User.EmailConfirmation ? "" : "error"
                  }
                >
                  ایمیل: {ticket.Customer.Person.Email}
                </Typography>
              </Box>
              <Divider />
            </>
          )}

          <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
            <Typography variant="h6">
              نوع دستگاه: {ticket.Device.DeviceType.Title}
            </Typography>
            <Typography variant="h6">
              برند دستگاه: {ticket.Device.DeviceBrand.Title}
            </Typography>
            <Typography ticket="h6">
              مدل دستگاه: {ticket.Device.Model}
            </Typography>
          </Box>
          <Box sx={{ mt: 2 }}>
            <Typography variant="h6">
              مشکل دستگاه: {ticket.Device.Description}
            </Typography>
            <Typography variant="h6">
              متعلقات دستگاه: {ticket.Device.Accessories}
            </Typography>
          </Box>
          {isAdmin && (
            <>
              <Box
                sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}
              >
                <Typography variant="h6">
                  قیمت حد استعلام: {ticket.InquiryPrice}
                </Typography>
                {ticket}
                <Typography variant="h6">
                  قیمت تعمیرکار: {ticket.RepairerPrice}
                </Typography>
                <Typography variant="h6">
                  قیمت فروشگاه: {ticket.ProfitMargin}
                </Typography>
                <Typography variant="h6">
                  قیمت نهایی: {ticket.FinalPrice}
                </Typography>
                <Typography variant="h6">
                  گارانتی :{ticket.deviceWaranty === true ? "دارد" : "ندارد"}
                </Typography>
              </Box>
              <Divider />
            </>
          )}

          {status.level === 0 && <WaitingStateModal />}
          {status.level === 1 && <CheckingStateModal />}

          {/* CHANGE */}
          {status.level === 2 && <CheckingStateModal />} 
          
          {status.level === 3 && <InquiryStateModal />}
          {status.level === 4 && <ReadyToRepairStateModal />}
          {status.level === 5 && <RepairingStateModal />}
          {(status.level === 6 || status.level === 7) && <ReadyStateModal />}

          <Stack mt={2} spacing={2} direction="row">
            <Button
              onClick={() => handleSubmit()}
              variant="contained"
              color="success"
            >
              {status.buttonText}
            </Button>
            <Button
              onClick={() => props.onClose()}
              variant="contained"
              color="error"
            >
              انصراف
            </Button>
          </Stack>
        </Paper>
      </Box>
    </Modal>
  );
}
