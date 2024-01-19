import { useState } from "react";
import PeopleDataGrid from "./PeopleDataGrid";
import AddEditPersonModal from "./modals/AddEditPersonModal";
import UserAccessModal from "./modals/UserAccessModal";
import EmailConfirmation from "../global/modals/EmailConfirmation";
import PhoneConfirmation from "../global/modals/PhoneConfirmation";

const PeopleManager = ({ personType, personTitle }) => {
  const [user, setUser] = useState(null);
  const [addEditPersonOpen, setAddEditPersonOpen] = useState(false);
  const [UserAccesOpen, setUserAccessOpen] = useState(false);
  const [emailConfirmOpen, setEmailConfirmOpen] = useState(false);
  const [phoneConfirmOpen, setPhoneConfirmOpen] = useState(false);
  const [isRefetching, setIsRefetching] = useState(false);

  function modalOpenHandler(type, payload) {
    switch (type) {
      case "addedit":
        setUser(payload);
        setAddEditPersonOpen(true);
        break;
      case "useraccess":
        setUser(payload);
        setUserAccessOpen(true);
        break;
      case "emailconfirm":
        setUser(payload);
        setEmailConfirmOpen(true);
        break;
      case "phoneconfirm":
        setUser(payload);
        setPhoneConfirmOpen(true);
        break;
      default:
        break;
    }
  }

  return (
    <>
      {addEditPersonOpen && (
        <AddEditPersonModal
          open={addEditPersonOpen}
          onClose={() => {
            setAddEditPersonOpen(false);
            setUser(null);
            setIsRefetching(true);
          }}
          data={user}
        />
      )}
      {UserAccesOpen && (
        <UserAccessModal
          open={UserAccesOpen}
          onClose={() => setUserAccessOpen(false)}
          user={user}
        />
      )}

      {emailConfirmOpen && (
        <EmailConfirmation
          open={emailConfirmOpen}
          onClose={() => setEmailConfirmOpen(false)}
          user={user}
        />
      )}

      {phoneConfirmOpen && (
        <PhoneConfirmation
          open={phoneConfirmOpen}
          onClose={() => setPhoneConfirmOpen(false)}
          user={user}
        />
      )}

      <h1>کاربران {personTitle}</h1>
      <PeopleDataGrid
        personType={personType}
        isRefetching={isRefetching}
        onSetIsRefetching={(value) => {
          setIsRefetching(value);
        }}
        modalHandler={(type, payload) => modalOpenHandler(type, payload)}
      />
    </>
  );
};

export default PeopleManager;
