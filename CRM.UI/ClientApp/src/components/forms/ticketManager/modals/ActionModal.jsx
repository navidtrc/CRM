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
  const [status] = useState(props.data.status);

  const [ticket, setTicket] = useState(null);
  const [isAdmin] = useState(true);
  const [privateDescription, setPrivateDescription] = useState(null);

  const step_0_data = useState({
    profit: null,
    repairer: { id: 0, label: "" },
  });

  useEffect(() => {
    TicketService.get(props.data.id).then((response) => {
      const result = response.data.Data.Data;
      setTicket(result);
    });
  }, []);

  const handleSubmit = () => {
    console.log(step_0_data);
  };

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
                    ticket.Customer.Person.User.PhoneNumberConfirmed
                      ? ""
                      : "error"
                  }
                  variant="h6"
                >
                  شماره تماس: {ticket.Customer.Person.User.PhoneNumber}
                </Typography>
                <Typography
                  color={
                    ticket.Customer.Person.User.EmailConfirmed ? "" : "error"
                  }
                >
                  ایمیل: {ticket.Customer.Person.User.Email}
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
                <Typography variant="h6">
                  قیمت تعمیرکار: {ticket?.RepairPrice}
                </Typography>
                <Typography variant="h6">
                  قیمت فروشگاه: {ticket?.ProfitMargin}
                </Typography>
                <Typography variant="h6">
                  قیمت نهایی: {ticket?.FinalPrice}
                </Typography>
                <Typography variant="h6">
                  گارانتی :{ticket.Device.Warranty === true ? "دارد" : "ندارد"}
                </Typography>
              </Box>
              <Divider />
            </>
          )}

          {status.step === 0 && <WaitingStateModal actionState={step_0_data} />}
          {status.step === 1 && <CheckingStateModal />}

          {status.step === 2 && <CheckingStateModal />}

          {status.step === 3 && <InquiryStateModal />}
          {status.step === 4 && <ReadyToRepairStateModal />}
          {status.step === 5 && <RepairingStateModal />}
          {(status.step === 6 || status.step === 7) && <ReadyStateModal />}

          <Box sx={{ mt: 2 }}>
            <TextField
              sx={{ m: 1 }}
              value={privateDescription}
              onChange={(e) => setPrivateDescription(e.target.value)}
              placeholder="توضیحات (خصوصی)"
              multiline
              rows={4}
              maxRows={5}
            />
          </Box>

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
