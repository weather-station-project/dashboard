import React, { useState } from 'react';
import { Button, Col, Form, Row } from 'react-bootstrap';
import { Field, FieldProps, Formik } from 'formik';
import {
  ChartValues,
  DefaultHistoricalDataRequest,
  HistoricalDataRequestValidationSchema,
  IHistoricalDataRequest,
} from '../../model/HistoricalDataTypes';
// https://react-bootstrap.github.io/forms/validation/
const HistoricalData: React.FC = () => {
  const [formValues] = useState<IHistoricalDataRequest>(DefaultHistoricalDataRequest);

  return (
    <Formik validationSchema={HistoricalDataRequestValidationSchema} onSubmit={console.log} initialValues={formValues}>
      {({ handleSubmit, handleChange, values, touched, errors }) => (
        <Form noValidate onSubmit={handleSubmit}>
          <Row className="mb-3 mt-5">
            <Form.Group as={Col} controlId="validationFormik01">
              <Form.Label>Initial date</Form.Label>
              <Form.Control
                type="date"
                name="initialDate"
                onChange={handleChange}
                isValid={touched.initialDate && !errors.initialDate}
              />
            </Form.Group>
            <Form.Group as={Col} controlId="validationFormik02">
              <Form.Label>Last name</Form.Label>
              <Form.Control
                type="date"
                name="finalDate"
                onChange={handleChange}
                isValid={touched.finalDate && !errors.finalDate}
              />
            </Form.Group>
          </Row>
          <Row className="mb-3">
            <Form.Group as={Col} controlId="validationFormik03">
              <Form.Label>Chart view</Form.Label>
              <Field name="chartView">
                {({ field, form: { setFieldValue } }: FieldProps) => (
                  <>
                    <Form.Check
                      type="radio"
                      id="chartView-lines"
                      checked={values.chartView === ChartValues.Lines}
                      name="chartView"
                      label={ChartValues.Lines}
                      onChange={() => setFieldValue(field.name, ChartValues.Lines, false)}
                    />
                    <Form.Check
                      type="radio"
                      id="chartView-bars"
                      checked={values.chartView === ChartValues.Bars}
                      name="chartView"
                      label={ChartValues.Bars}
                      onChange={() => setFieldValue(field.name, ChartValues.Bars, false)}
                    />
                  </>
                )}
              </Field>
            </Form.Group>
          </Row>
          <Button type="submit">Submit form</Button>
        </Form>
      )}
    </Formik>
  );
};

export default HistoricalData;
