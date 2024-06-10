import axios from "axios";
import { APIURL } from "../Constants";

export default axios.create({ baseURL: APIURL });
