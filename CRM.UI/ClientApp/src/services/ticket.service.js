import axios from "axios";



const prerequisite = () => {
  return axios.get("/api/ticket/prerequisite");
};

const get = (id) => {
  return axios.get(`/api/ticket/get?id=${id}`);
};

const getRepairers = () => {
  return axios.get(`/api/people/GetByRole`);
}


const TicketService = {
  prerequisite,
  get,
  getRepairers
}

export default TicketService;
