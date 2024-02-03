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
  // const [status] = useState(props.data.statusId);
  const [status] = useState(2); //test
  const [data, setData] = useState(null);

  useEffect(() => {
    TicketService.get(props.data.id).then((response) => {
      const result = response.data.Data.Data;
      debugger;
      setData({
        ...props,
        data: {
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
        },
      });
    });
  }, []);

  if (data === null) {
    return "Loading";
  }
  if (status === 7) {
    Swal.fire("تیکت بسته شده و فرآیند آن به پایان رسیده");
    return;
  }
  return (
    <>
      {status === 0 && <WaitingStateModal {...data} />}
      {status === 1 && <CheckingStateModal {...data} />}
      {status === 2 && <InquiryStateModal {...data} />}
      {status === 3 && <ReadyToRepairStateModal {...data} />}
      {status === 4 && <RepairingStateModal {...data} />}
      {(status === 5 || status === 6) && <ReadyStateModal {...data} />}
    </>
  );
}
