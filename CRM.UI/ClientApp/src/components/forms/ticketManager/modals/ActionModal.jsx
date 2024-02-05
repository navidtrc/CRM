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

const statusButtonText = (level) => {
  switch (level) {
    case 0:
      return "ارسال برای بررسی تعمیر کار";
    case 1:
      return "اعلام نتیجه بررسی";
    case 2:
      return "ادامه";
    case 3:
      return "شروع تعمیر";
    case 4:
      return "اتمام تعمیر و ارسال به مرکز فروش";
    case 5:
      return "تحویل داده شد";
    case 6:
      return "تحویل داده شد";
    default:
      break;
  }
};

export default function ActionModal(props) {
  const [status] = useState({
    level: 2, // props.data.statusId
    buttonText: statusButtonText(props.data.statusId),
  });

  const [ticket, setTicket] = useState(null);
  const [isAdmin] = useState(true);

  useEffect(() => {
    TicketService.get(props.data.id).then((response) => {
      const result = response.data.Data.Data;
      debugger;
      setTicket({
        ticketId: result.Id,
        ticketNumber: result.Number,
        ticketDate: new Date(result.Date),
        ticketPersianDate: result.PersianDate,
        customerName: result.Customer.Person.Name,
        customerPhone: result.Customer.Person.User.PhoneNumber,
        customerEmail: result.Customer.Person.User.Email,
        customerPhoneConfirmation:
          result.Customer.Person.User.PhoneNumberConfirmed,
        customerEmailConfirmation: result.Customer.Person.User.EmailConfirmed,
        deviceType: result.Device.DeviceType.Title,
        deviceBrand: result.Device.DeviceBrand.Title,
        deviceModel: result.Device.Model,
        deviceDescrption: result.Device.Description,
        deviceAccessories: result.Device.Accessories,
        deviceWaranty: result.Device.Warranty,
        inquiryPrice: result.InquiryPrice,
        repairer: {
          id: result.RepairerId,
          label: result.Repairer.Person.Name,
        },
        privateDescription: result.PrivateDescription,
      });
    });
  }, []);

  const handleSubmit = () => {};

  if (props.data === null) {
    return "Loading";
  }
  if (status.level === 7) {
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
            تیکت در صف انتظار
          </Typography>
          <Divider />
          <Box sx={{ m: 2, display: "flex", justifyContent: "space-between" }}>
            <Typography variant="h6">
              شماره تیکت: {ticket.ticketNumber}
            </Typography>
            <Typography variant="h6">
              تاریخ تیکت: {ticket.ticketPersianDate}
            </Typography>
          </Box>
          <Divider />
          {isAdmin && (
            <>
              <Box
                sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}
              >
                <Typography variant="h6">
                  نام مشتری: {ticket.customerName}
                </Typography>
                <Typography
                  color={ticket.customerPhoneConfirmation ? "" : "error"}
                  variant="h6"
                >
                  شماره تماس: {ticket.customerPhone}
                </Typography>
                <Typography
                  color={ticket.customerPhoneConfirmation ? "" : "error"}
                >
                  ایمیل: {ticket.customerEmail}
                </Typography>
              </Box>
              <Divider />
            </>
          )}

          <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
            <Typography variant="h6">
              نوع دستگاه: {ticket.deviceType}
            </Typography>
            <Typography variant="h6">
              برند دستگاه: {ticket.deviceBrand}
            </Typography>
            <Typography ticket="h6">
              مدل دستگاه: {ticket.deviceModel}
            </Typography>
          </Box>
          <Box sx={{ mt: 2 }}>
            <Typography variant="h6">
              مشکل دستگاه: {ticket.deviceDescrption}
            </Typography>
            <Typography variant="h6">
              متعلقات دستگاه: {ticket.deviceAccessories}
            </Typography>
          </Box>
          {isAdmin && (
            <>
              <Box
                sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}
              >
                <Typography variant="h6">
                  قیمت برآورد شده: {ticket.inquiryPrice}
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
          {status.level === 2 && <InquiryStateModal />}
          {status.level === 3 && <ReadyToRepairStateModal />}
          {status.level === 4 && <RepairingStateModal />}
          {(status.level === 5 || status.level === 6) && <ReadyStateModal />}

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
