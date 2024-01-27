import { useState } from "react";
import TicketDataGrid from "./TicketDataGrid";
import AddEditTicketModal from "./modals/AddEditTicketModal";
import EmailConfirmation from "../global/modals/EmailConfirmation";
import PhoneConfirmation from "../global/modals/PhoneConfirmation";

const TicketManager = ({ personType, personTitle }) => {
  const [ticket, setTicket] = useState(null);
  const [addEditPersonOpen, setAddEditPersonOpen] = useState(false);
  const [emailConfirmOpen, setEmailConfirmOpen] = useState(false);
  const [phoneConfirmOpen, setPhoneConfirmOpen] = useState(false);
  const [isRefetching, setIsRefetching] = useState(false);

  function modalOpenHandler(type, payload) {
    switch (type) {
      case "addedit":
        setTicket(payload);
        setAddEditPersonOpen(true);
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
      {addEditPersonOpen && (
        <AddEditTicketModal
          open={addEditPersonOpen}
          onClose={() => {
            setAddEditPersonOpen(false);
            setTicket(null);
            setIsRefetching(true);
          }}
          data={ticket}
        />
      )}

      {/* {emailConfirmOpen && (
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
      )} */}

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
