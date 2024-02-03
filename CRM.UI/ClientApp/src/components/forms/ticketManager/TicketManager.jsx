import { useState } from "react";
import TicketDataGrid from "./TicketDataGrid";
import AddEditTicketModal from "./modals/AddEditTicketModal";
import EmailConfirmation from "../global/modals/EmailConfirmation";
import PhoneConfirmation from "../global/modals/PhoneConfirmation";
import WaitingStateModal from "./modals/WaitingStateModal";
import CheckingStateModal from "./modals/CheckingStateModal";
import InquiryStateModal from "./modals/InquiryStateModal";
import ReadyToRepairStateModal from "./modals/ReadyToRepairStateModal";
import FellowTimeline from "./modals/FellowTimelineModal";
import FellowTimelineModal from "./modals/FellowTimelineModal";
import ActionModal from "./modals/ActionModal";

const TicketManager = ({ personType, personTitle }) => {
  const [ticket, setTicket] = useState(null);
  const [addEditTicketOpen, setAddEditTicketOpen] = useState(false);
  const [fellowTicketOpen, setFellowTicketOpen] = useState(false);
  const [actionTicketOpen, setActionTicketOpen] = useState(false);
  const [emailConfirmOpen, setEmailConfirmOpen] = useState(false);
  const [phoneConfirmOpen, setPhoneConfirmOpen] = useState(false);
  const [isRefetching, setIsRefetching] = useState(false);

  function modalOpenHandler(type, payload) {
    switch (type) {
      case "addedit":
        setTicket(payload);
        setAddEditTicketOpen(true);
        break;
      case "fellow":
        setTicket(payload);
        setFellowTicketOpen(true);
        break;
      case "action":
        setTicket(payload);
        setActionTicketOpen(true);
        break;
      case "emailconfirm":
        setTicket(payload);
        setEmailConfirmOpen(true);
        break;
      case "phoneconfirm":
        setTicket(payload);
        setPhoneConfirmOpen(true);
        break;
      default:
        break;
    }
  }

  return (
    <>
      {addEditTicketOpen && (
        <AddEditTicketModal
          open={addEditTicketOpen}
          onClose={() => {
            setAddEditTicketOpen(false);
            setTicket(null);
            setIsRefetching(true);
          }}
          data={ticket}
        />
      )}

      {fellowTicketOpen && (
        <FellowTimelineModal
          open={fellowTicketOpen}
          onClose={() => {
            setFellowTicketOpen(false);
            setTicket(null);
            setIsRefetching(true);
          }}
          data={ticket}
        />
      )}

      {actionTicketOpen && (
        <ActionModal
          open={actionTicketOpen}
          onClose={() => {
            setActionTicketOpen(false);
            setTicket(null);
            setIsRefetching(true);
          }}
          data={ticket}
        />
      )}

      {emailConfirmOpen && (
        <EmailConfirmation
          open={emailConfirmOpen}
          onClose={() => setEmailConfirmOpen(false)}
          user={ticket}
        />
      )}

      {phoneConfirmOpen && (
        <PhoneConfirmation
          open={phoneConfirmOpen}
          onClose={() => setPhoneConfirmOpen(false)}
          user={ticket}
        />
      )}

      <h1>تیکت ها {personTitle}</h1>
      <TicketDataGrid
        isRefetching={isRefetching}
        onSetIsRefetching={(value) => {
          setIsRefetching(value);
        }}
        modalHandler={(type, payload) => modalOpenHandler(type, payload)}
      />
    </>
  );
};

export default TicketManager;
